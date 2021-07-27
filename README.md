# Unity Attributes
Provides custom attributes to work with the Unity game engine.

## Installing
You can install this package through the Unity Package Manager using the git url.

`https://github.com/LordArugula/Unity-Extensions.git`

## Attributes

### [InjectAttribute]
Allows injecting dependencies via the Inspector to fields decorated with the [SerializeReference](https://docs.unity3d.com/Documentation/ScriptReference/SerializeReference.html) attribute.

Any limitations of the [SerializeReference] attribute applies to the [Inject] attribute as well. For example, applying changes to a prefab instance is not supported due to the way Unity serializes prefab modifications. See [issue](https://issuetracker.unity3d.com/issues/cannot-override-type-of-serializedreference-fields-in-prefab-instances).

```cs
using Arugula.Extensions;

public interface IService { }

public class FooService : IService { }

public class BarService : IService { }

public class InjectAttributeDemo : MonoBehaviour
{
    [Inject]
    [SerializeReference]
    public IService service;

    // Works on lists and arrays
    [Inject]
    [SerializeReference]
    public List<IService> services;
}
```

![InjectAttribute gif](Documentation~/images/Inject.gif)

### [ReadOnlyAttribute]
Prevents editing fields via the Inspector during Play Mode, Edit Mode, or both.

```cs
using Arugula.Extensions;

public class ReadOnlyAttributeDemo : MonoBehaviour
{
    [ReadOnly]
    public GameObject readOnlyAlways;

    [ReadOnly(playMode: true)]
    public int readOnlyDuringPlayMode;
    
    [ReadOnly(editMode: true)]
    public float readOnlyDuringEditMode;
}
```

![ReadOnlyAttribute gif](Documentation~/images/ReadOnly.gif)

### [SceneAttribute]
Makes integer or string fields a scene selection field.

```cs
using Arugula.Extensions;

public class SceneAttributeDemo : MonoBehaviour
{
    [Scene]
    public int mainScene;

    [Scene]
    public string otherScene;
}
```

![SceneAttribute gif](Documentation~/images/Scene.gif)

### [LayerAttribute]
Makes integer or string fields a layer selection field.

```cs
using Arugula.Extensions;

public class LayerAttributeDemo : MonoBehaviour
{
    [Layer]
    public int groundLayer;

    [Layer]
    public string waterLayer;
}
```

![LayerAttribute gif](Documentation~/images/Layer.gif)

### [TagAttribute]
Makes string fields a tag selection field.

```cs
using Arugula.Extensions;

public class TagAttributeDemo : MonoBehaviour
{
    [Tag]
    public string playerTag;
}
```

![TagAttribute gif](Documentation~/images/Tag.gif)

### [PrefabAttribute]
Restricts a GameObject or Component field to only accept prefab assets.

```cs
using Arugula.Extensions;

public class PrefabAttributeDemo : MonoBehaviour
{
    [Prefab]
    public GameObject player;

    [Prefab]
    public Rigidbody projectile;
}
```

![PrefabAttribute gif](Documentation~/images/Prefab.gif)

## Reference Attributes
These attributes help stop you from leaving a reference field unassigned. They only apply to fields on a type deriving from MonoBehaviour or ScriptableObject. 

The [RequireReference] attribute forces you to assign the field before entering Play Mode.

The [GetComponent], [FindGameObject], and [FindAsset] attributes automatically search for an object and assigns it to the field with the attribute. If nothing is found, they leave a warning in the Console Window. References get assigned whenever Unity reloads, the Hierarchy changes, or an asset is imported.

### [RequireReferenceAttribute]
Forces a reference type field to be assigned before entering Play Mode.

```cs
using Arugula.Extensions;

public class RequireReferenceAttributeDemo : MonoBehaviour
{
    [RequireReference]
    public Animator animator;

    [RequireReference]
    public GameObject prefab;

    [RequireReference, SerializeReference, Inject]
    public IService list;

    public Test test;
}

public class Test
{
    // RequireReference does nothing in this case
    [RequireReference]
    public GameObject gameObject;
}
```

### [FindGameObjectAttribute]
Searches for a GameObject in the scene and assigns it.

```cs
using Arugula.Extensions;

public class FindGameObjectAttributeDemo : MonoBehaviour
{
    [FindGameObject(Tag = "Player")]
    public GameObject player;

    [FindGameObject(Name = "Main Camera")]
    public Camera mainCamera;

    [FindGameObject(CreateInstance = true)]
    public GameManager gameManager;
}
```

### [FindAssetAttribute]
Searches for an asset and assigns it.

```cs
using Arugula.Extensions;

public class FindAssetAttributeDemo : MonoBehaviour
{
    // Component and GameObject fields search for a 
    // prefab asset with the Component, if applicable.
    [FindAsset(Name = "Player")]
    public GameObject player;

    [FindAsset(Name = "Bullet", CreateAsset = true)]
    public Rigidbody bullet;

    [FindAsset]
    public GameManager prefab;

    // CreateAsset creates a default asset for ScriptableObjects and Components and GameObjects.
    [FindAsset(CreateAsset = true)]
    public SomeScriptableObject scriptableObject;
}

```

### [GetComponentAttribute]
Gets a Component on the GameObject or its children and assigns it.

```cs
public class GetComponentAttributeDemo : MonoBehaviour
{
    [GetComponent(AddComponent = true)]
    public Rigidbody _rigidbody;

    [GetComponent(IncludeChildren = true)]
    public Animator _animator;
}
```
