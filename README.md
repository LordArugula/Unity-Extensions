# Unity Attributes
Provides custom attributes to work with the Unity game engine.

## Installing
You can install this package through the Unity Package Manager using the git url.

`https://github.com/LordArugula/Unity-Extensions.git`

## Attributes

### [InjectAttribute]
Allows injecting dependencies via the Inspector to fields decorated with the [SerializeReference](https://docs.unity3d.com/Documentation/ScriptReference/SerializeReference.html) attribute.

```cs
using Arugula.Extensions;

public interface IService { }

public class FooService : IService { }

public class BarService : IService { }

public class InjectAttributeDemo : MonoBehaviour
{
    [SerializeReference]
    [Inject]
    public IService service;
}
```

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
