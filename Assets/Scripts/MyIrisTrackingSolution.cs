// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.IrisTracking
{
  public class MyIrisTrackingSolution : MonoBehaviour
  {

    [SerializeField] private Screen _screen;
    [SerializeField] private DetectionListAnnotationController _faceDetectionsAnnotationController;
    [SerializeField] private NormalizedRectAnnotationController _faceRectAnnotationController;
    [SerializeField] private FaceLandmarkListAnnotationController _faceLandmarksWithIrisAnnotationController;
    [SerializeField] private IrisTrackingGraph _graphRunner;
    [SerializeField] private TextureFramePool _textureFramePool;

    private Coroutine _coroutine;

    public RunningMode runningMode;

    public long timeoutMillisec
    {
      get => _graphRunner.timeoutMillisec;
      set => _graphRunner.SetTimeoutMillisec(value);
    }

#pragma warning disable IDE1006
    // TODO: make it static
    protected virtual string TAG => GetType().Name;
#pragma warning restore IDE1006

    protected bool isPaused;

    protected void Start()
    {
      Play();
    }

    /// <summary>
    ///   Start the main program from the beginning.
    /// </summary>
    public  void Play()
    {
      if (_coroutine != null)
      {
        Stop();
      }
 
      _coroutine = StartCoroutine(Run());

      isPaused = false;
    }

    /// <summary>
    ///   Pause the main program.
    /// </summary>
    public void Pause()
    {
      isPaused = true;
      MyImageSourceProvider.ImageSource.Pause();
    }

    /// <summary>
    ///    Resume the main program.
    ///    If the main program has not begun, it'll do nothing.
    /// </summary>
    public void Resume()
    {
      isPaused = false;
      var _ = StartCoroutine(MyImageSourceProvider.ImageSource.Resume());
    }

    /// <summary>
    ///   Stops the main program.
    /// </summary>
    public virtual void Stop()
    {
      isPaused = true;
      StopCoroutine(_coroutine);
      MyImageSourceProvider.ImageSource.Stop();
      _graphRunner.Stop();
    }

    private IEnumerator Run()
    {
      var graphInitRequest = _graphRunner.WaitForInit();
      MyImageSourceProvider.SwitchSource(ImageSource.SourceType.RenderTexture);
      var imageSource = MyImageSourceProvider.ImageSource;

      yield return imageSource.Play();

      if (!imageSource.isPrepared)
      {
        Logger.LogError(TAG, "Failed to start ImageSource, exiting...");
        yield break;
      }
      // NOTE: The _screen will be resized later, keeping the aspect ratio.
      _screen.Initialize(imageSource);

      Logger.LogInfo(TAG, $"Running Mode = {runningMode}");

      yield return graphInitRequest;
      if (graphInitRequest.isError)
      {
        Logger.LogError(TAG, graphInitRequest.error);
        yield break;
      }

      if (runningMode == RunningMode.Async)
      {
        _graphRunner.OnFaceDetectionsOutput.AddListener(OnFaceDetectionsOutput);
        _graphRunner.OnFaceRectOutput.AddListener(OnFaceRectOutput);
        _graphRunner.OnFaceLandmarksWithIrisOutput.AddListener(OnFaceLandmarksWithIrisOutput);
        _graphRunner.StartRunAsync(imageSource).AssertOk();
      }
      else
      {
        _graphRunner.StartRun(imageSource).AssertOk();
      }

      // Use RGBA32 as the input format.
      // TODO: When using GpuBuffer, MediaPipe assumes that the input format is BGRA, so the following code must be fixed.
      _textureFramePool.ResizeTexture(imageSource.textureWidth, imageSource.textureHeight, TextureFormat.RGBA32);

      SetupAnnotationController(_faceDetectionsAnnotationController, imageSource);
      SetupAnnotationController(_faceRectAnnotationController, imageSource);
      SetupAnnotationController(_faceLandmarksWithIrisAnnotationController, imageSource);

      while (true)
      {
        yield return new WaitWhile(() => isPaused);

        var textureFrameRequest = _textureFramePool.WaitForNextTextureFrame();
        yield return textureFrameRequest;
        var textureFrame = textureFrameRequest.result;

        // Copy current image to TextureFrame
        ReadFromImageSource(imageSource, textureFrame);

        _graphRunner.AddTextureFrameToInputStream(textureFrame).AssertOk();

        if (runningMode == RunningMode.Sync)
        {
          // TODO: copy texture before `textureFrame` is released
          _screen.ReadSync(textureFrame);

          // When running synchronously, wait for the outputs here (blocks the main thread).
          var value = _graphRunner.FetchNextValue();
          _faceDetectionsAnnotationController.DrawNow(value.faceDetections);
          _faceRectAnnotationController.DrawNow(value.faceRect);
          _faceLandmarksWithIrisAnnotationController.DrawNow(value.faceLandmarksWithIris);
        }

        yield return new WaitForEndOfFrame();
      }
    }

    private void OnFaceDetectionsOutput(List<Detection> faceDetections)
    {
      _faceDetectionsAnnotationController.DrawLater(faceDetections);
    }

    private void OnFaceRectOutput(NormalizedRect faceRect)
    {
      _faceRectAnnotationController.DrawLater(faceRect);
    }

    private void OnFaceLandmarksWithIrisOutput(NormalizedLandmarkList faceLandmarkListWithIris)
    {
      _faceLandmarksWithIrisAnnotationController.DrawLater(faceLandmarkListWithIris);
    }

    protected static void SetupAnnotationController<T>(AnnotationController<T> annotationController, ImageSource imageSource, bool expectedToBeMirrored = false) where T : HierarchicalAnnotation
    {
      annotationController.isMirrored = expectedToBeMirrored ^ imageSource.isHorizontallyFlipped ^ imageSource.isFrontFacing;
      annotationController.rotationAngle = imageSource.rotation.Reverse();
    }

    protected static void ReadFromImageSource(ImageSource imageSource, TextureFrame textureFrame)
    {
      var sourceTexture = imageSource.GetCurrentTexture();

      // For some reason, when the image is coiped on GPU, latency tends to be high.
      // So even when OpenGL ES is available, use CPU to copy images.
      var textureType = sourceTexture.GetType();

      if (textureType == typeof(WebCamTexture))
      {
        textureFrame.ReadTextureFromOnCPU((WebCamTexture)sourceTexture);
      }
      else if (textureType == typeof(Texture2D))
      {
        textureFrame.ReadTextureFromOnCPU((Texture2D)sourceTexture);
      }
      else
      {
        textureFrame.ReadTextureFromOnCPU(sourceTexture);
      }
    }
  }
}
