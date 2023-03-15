using UnityEngine;
using Application = UnityEngine.Application;

namespace Solitaire
{
    public class ProjectSetup

    {
    private readonly ProjectSettings _settings;

    public ProjectSetup(ProjectSettings settings)
    {
        _settings = settings;
    }

    public void Initialize()
    {
        Application.targetFrameRate = _settings.TargetFps;
        Input.multiTouchEnabled = _settings.IsMultitouch;
    }
    }
}