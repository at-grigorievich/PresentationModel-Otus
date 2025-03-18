using Core;
using Core.Storage;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PopupSceneScope : LifetimeScope
{
    [SerializeField] private CharacterStorageFactory characterStorageFactory;
    [SerializeField] private CharacterCardPresenterFactory characterCardPresenterFactory;

    protected override void Configure(IContainerBuilder builder)
    {
        characterStorageFactory.Create(builder);
        characterCardPresenterFactory.Create(builder);
    }
}