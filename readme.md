# Noix - a .net noise library

A library for different type of noise functions

## Implemented

### Perlin Noise

Perlin1d:

```csharp
            var perlin1d = new Perlin1D(50, width);
            var perlinValues = perlin1d.GetValues(0, width).ToArray();
```

Perlin2d:

```csharp
            var width = 800;
            var height = 600;
            var perlin2d = new Perlin2D(100, width, height);
            using (var writer = new BinaryWriter(File.Open("/home/vegardb/perlin.data", FileMode.Create)))
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        var val = perlin2d.GetValue(x, y);
                        var valB = (Byte)(((val + 1) / 2) * 255);
                        writer.Write(valB);
                    }
                }
            }
```

### Change random number generator and/or interpolator

Default the perlin noise functions will use a standard rng and a smoothstep interpolator. Both can be changed, by implementing the IRandomGenerator or the IInterpolator interface.
You can see the code for the default classes here:  
[Default Random Generator](./Fantasista.Noix/DefaultRandomGenerator.cs)  
[Smoothstep Interpolator](Fantasista.Noix/SmoothStepInterpolater.cs)

To change the Perlin Noise classes to use a different interpolators, use the constructor that has parameters for these:

```csharp
            var perlin2d = new Perlin2D(100, width, height, new DefaultRandomGenerator(1), new SmoothStepInterpolater());
```
