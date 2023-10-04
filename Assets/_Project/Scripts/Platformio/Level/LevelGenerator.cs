using System;
using Platformio.DI;
using Platformio.Environment;
using Platformio.Sound;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Platformio.Level
{
    public class LevelGenerator
    {
        [Inject] private readonly Settings _settings;

        public Object GetLevelPrefab(InjectContext context)
        {
            return _settings.levelPrefabs.GetRandomItem();
        }

        public void InjectLevelGameObject(DiContainer container)
        {
            var theme = _settings.themes.GetRandomItem();
            
            container.BindInstance(theme);
            container.BindInstance(theme.stepsSounds).WhenInjectedInto<StepSoundPlayer>();
            container.BindInterfacesAndSelfTo<StepSoundPlayer>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public EnvironmentThemeConfiguration[] themes;
            public GameObject[] levelPrefabs;
        }
    }
}