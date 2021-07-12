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

public class Test : MonoBehaviour
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

public class Test : MonoBehaviour
{
    [ReadOnly]
    public GameObject readOnlyAlways;
    [ReadOnly(PlayMode = true)]
    public int readOnlyDuringPlayMode;
    [ReadOnly(EditMode = true)]
    public float readOnlyDuringEditMode
}
```

### [SceneAttribute]
Makes an integer field a scene selection field.

```cs
using Arugula.Extensions;

public class Test : MonoBehaviour
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

public class Test : MonoBehaviour
{
    [Layer]
    public int groundLayer;

    [Layer]
    public string waterLayer;
}
```

### [TagAttribute]
Makes a string field a tag selection field.

```cs
using Arugula.Extensions;

public class Test : MonoBehaviour
{
    [Tag]
    public string playerTag;
}
```
