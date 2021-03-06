# Benchmarks
This repository for its own microbenchmarks

## EF7 AsNoTracking vs AsTracking
``` ini

BenchmarkDotNet=v0.10.12, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Intel Core i5 CPU 750 2.67GHz (Nehalem), 1 CPU, 4 logical cores and 4 physical cores
Frequency=2602315 Hz, Resolution=384.2732 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host] : .NET Core 2.0.0 (Framework 4.6.00001.0), 32bit RyuJIT
  Core   : .NET Core 2.0.0 (Framework 4.6.00001.0), 32bit RyuJIT

Job=Core  Runtime=Core  

```
|  Method | Records |   DbType |       Mean |     Error |    StdDev | Scaled |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|-------- |-------- |--------- |-----------:|----------:|----------:|-------:|---------:|--------:|--------:|----------:|
|   **Track** |       **1** | **InMemory** | **1,997.5 us** | **13.308 us** | **12.448 us** |   **1.00** |  **42.9688** | **23.4375** |  **7.8125** | **166.06 KB** |
| NoTrack |       1 | InMemory | 1,766.5 us | 10.005 us |  8.355 us |   0.88 |  42.9688 | 21.4844 |  9.7656 | 164.92 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |       **1** | **SqlLight** |   **962.1 us** |  **4.921 us** |  **4.362 us** |   **1.00** |   **3.9063** |       **-** |       **-** |  **19.45 KB** |
| NoTrack |       1 | SqlLight |   950.4 us |  1.304 us |  1.156 us |   0.99 |   3.9063 |       - |       - |  18.95 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |      **10** | **InMemory** | **1,991.6 us** |  **4.890 us** |  **4.574 us** |   **1.00** |  **44.9219** | **21.4844** |  **9.7656** | **169.07 KB** |
| NoTrack |      10 | InMemory | 1,835.1 us |  3.521 us |  3.294 us |   0.92 |  42.9688 | 21.4844 |  9.7656 | 165.53 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |      **10** | **SqlLight** | **1,011.0 us** |  **2.834 us** |  **2.512 us** |   **1.00** |   **5.8594** |       **-** |       **-** |  **24.33 KB** |
| NoTrack |      10 | SqlLight |   968.5 us |  4.634 us |  3.870 us |   0.96 |   3.9063 |       - |       - |  20.74 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |     **100** | **InMemory** | **2,195.0 us** |  **7.693 us** |  **7.196 us** |   **1.00** |  **50.7813** | **27.3438** |  **7.8125** | **202.41 KB** |
| NoTrack |     100 | InMemory | 1,884.8 us |  7.144 us |  6.333 us |   0.86 |  42.9688 | 19.5313 |  9.7656 | 166.59 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |     **100** | **SqlLight** | **1,423.6 us** |  **8.378 us** |  **7.837 us** |   **1.00** |  **17.5781** |       **-** |       **-** |  **74.24 KB** |
| NoTrack |     100 | SqlLight | 1,091.7 us |  5.925 us |  5.542 us |   0.77 |   7.8125 |       - |       - |  38.19 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |    **1000** | **InMemory** | **4,997.9 us** | **30.450 us** | **28.483 us** |   **1.00** | **101.5625** | **39.0625** | **15.6250** | **534.86 KB** |
| NoTrack |    1000 | InMemory | 1,957.9 us |  3.175 us |  2.652 us |   0.39 |  42.9688 | 23.4375 |  7.8125 | 173.64 KB |
|         |         |          |            |           |           |        |          |         |         |           |
|   **Track** |    **1000** | **SqlLight** | **5,880.8 us** | **43.999 us** | **41.156 us** |   **1.00** | **117.1875** | **39.0625** |       **-** | **575.44 KB** |
| NoTrack |    1000 | SqlLight | 2,417.2 us | 11.036 us | 10.323 us |   0.41 |  50.7813 | 11.7188 |       - | 214.11 KB |
