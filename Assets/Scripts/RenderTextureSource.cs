using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Mediapipe.Unity
{
  public class RenderTextureSource : ImageSource
  {
    [SerializeField] private RenderTexture[] _availableSources;
    [SerializeField] private ResolutionStruct[] _defaultAvailableResolutions;

    private Texture2D _outputTexture;
    private RenderTexture _renderTexture;
    private RenderTexture renderTexture
    {
      get => _renderTexture;
      set
      {
        _renderTexture = value;
        resolution = GetDefaultResolution();
      }
    }

    public override SourceType type => SourceType.Image;

    public override double frameRate => 0;

    public override string sourceName => renderTexture != null ? renderTexture.name : null;

    public override string[] sourceCandidateNames => _availableSources?.Select(source => source.name).ToArray();

    public override ResolutionStruct[] availableResolutions => _defaultAvailableResolutions;

    public override bool isPrepared => _outputTexture != null;

    private bool _isPlaying = false;
    public override bool isPlaying => _isPlaying;

    private void Start()
    {
      if (_availableSources != null && _availableSources.Length > 0)
      {
        renderTexture = _availableSources[0];
      }
    }

    public override void SelectSource(int sourceId)
    {
      if (sourceId < 0 || sourceId >= _availableSources.Length)
      {
        throw new ArgumentException($"Invalid source ID: {sourceId}");
      }

      renderTexture = _availableSources[sourceId];
    }

    public override IEnumerator Play()
    {
      if (renderTexture == null)
      {
        throw new InvalidOperationException("Image is not selected");
      }
      if (isPlaying)
      {
        yield break;
      }

      InitializeOutputTexture(renderTexture);
      _isPlaying = true;
      yield return null;
    }

    public override IEnumerator Resume()
    {
      if (!isPrepared)
      {
        throw new InvalidOperationException("Image is not prepared");
      }
      _isPlaying = true;

      yield return null;
    }

    public override void Pause()
    {
      _isPlaying = false;
    }
    public override void Stop()
    {
      _isPlaying = false;
      _outputTexture = null;
    }

    public override Texture GetCurrentTexture()
    {
      return _outputTexture;
    }

    private ResolutionStruct GetDefaultResolution()
    {
      var resolutions = availableResolutions;

      return (resolutions == null || resolutions.Length == 0) ? new ResolutionStruct() : resolutions[0];
    }

    private void InitializeOutputTexture(Texture src)
    {
      _outputTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

      Texture resizedTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
      // TODO: assert ConvertTexture finishes successfully
      var _ = Graphics.ConvertTexture(src, resizedTexture);

      var currentRenderTexture = RenderTexture.active;
      var tmpRenderTexture = new RenderTexture(resizedTexture.width, resizedTexture.height, 32);
      Graphics.Blit(resizedTexture, tmpRenderTexture);
      RenderTexture.active = tmpRenderTexture;

      var rect = new UnityEngine.Rect(0, 0, _outputTexture.width, _outputTexture.height);
      _outputTexture.ReadPixels(rect, 0, 0);
      _outputTexture.Apply();

      RenderTexture.active = currentRenderTexture;
    }
  }
}