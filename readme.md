| Method                                                    | Mean            | Error        | StdDev       | Gen0   | Allocated |
|---------------------------------------------------------- |----------------:|-------------:|-------------:|-------:|----------:|
| <b>Bulkhead</b> |
| Bulkhead_Sync                                     |  68.87 ns | 0.349 ns | 0.292 ns | 0.0148 |     248 B |
| Bulkhead_Async                                    | 109.24 ns | 2.129 ns | 2.366 ns | 0.0238 |     400 B |
| Bulkhead_Async_With_CancellationToken             | 111.92 ns | 2.174 ns | 2.233 ns | 0.0238 |     400 B |
| Bulkhead_Sync_With_Result                         |  61.45 ns | 1.251 ns | 1.536 ns | 0.0095 |     160 B |
| Bulkhead_Async_With_Result                        |  85.67 ns | 1.730 ns | 1.923 ns | 0.0095 |     160 B |
| Bulkhead_Async_With_Result_With_CancellationToken |  86.72 ns | 0.251 ns | 0.234 ns | 0.0095 |     160 B |
| <b>Circuit-breaker</b> |
| CircuitBreaker_Sync_Succeeds              |  57.18 ns | 0.212 ns | 0.177 ns | 0.0220 |     368 B |
| CircuitBreaker_Async_Succeeds             | 129.27 ns | 0.948 ns | 0.887 ns | 0.0396 |     664 B |
| CircuitBreaker_Sync_With_Result_Succeeds  |  49.23 ns | 0.363 ns | 0.322 ns | 0.0167 |     280 B |
| CircuitBreaker_Async_With_Result_Succeeds | 101.80 ns | 0.777 ns | 0.727 ns | 0.0253 |     424 B |
| <b>Fallback</b> |
| Fallback_Sync_Succeeds  |    25.77 ns |  0.463 ns |  0.433 ns | 0.0110 |     184 B |
| Fallback_Async_Succeeds |    50.84 ns |  0.205 ns |  0.191 ns | 0.0110 |     184 B |
| Fallback_Sync_Throws    | 6,089.66 ns | 13.646 ns | 12.097 ns | 0.0381 |     696 B |
| Fallback_Async_Throws   | 6,239.74 ns | 95.669 ns | 89.489 ns | 0.0381 |     696 B |
| <b>Rate-limit</b> |
| RateLimit_Sync_Succeeds              | 38.13 ns | 0.208 ns | 0.194 ns | 0.0148 |     248 B |
| RateLimit_Async_Succeeds             | 77.02 ns | 0.252 ns | 0.211 ns | 0.0238 |     400 B |
| RateLimit_Sync_With_Result_Succeeds  | 28.66 ns | 0.266 ns | 0.236 ns | 0.0095 |     160 B |
| RateLimit_Async_With_Result_Succeeds | 56.30 ns | 0.266 ns | 0.249 ns | 0.0095 |     160 B |
| <b>Retry</b> |
| Retry_Sync_Succeeds                                     |     43.80 ns |   0.236 ns |   0.197 ns | 0.0200 |     336 B |
| Retry_Async_Succeeds                                    |     85.57 ns |   0.538 ns |   0.503 ns | 0.0291 |     488 B |
| Retry_Async_Succeeds_With_CancellationToken             |     87.41 ns |   1.082 ns |   0.959 ns | 0.0291 |     488 B |
| Retry_Sync_With_Result_Succeeds                         |     38.22 ns |   0.796 ns |   2.011 ns | 0.0148 |     248 B |
| Retry_Async_With_Result_Succeeds                        |     70.59 ns |   1.018 ns |   0.953 ns | 0.0148 |     248 B |
| Retry_Async_With_Result_Succeeds_With_CancellationToken |     66.42 ns |   0.717 ns |   0.671 ns | 0.0148 |     248 B |
| Retry_Sync_Throws_Then_Succeeds                         |  6,952.04 ns |  66.969 ns |  62.643 ns | 0.0687 |    1176 B |
| Retry_Async_Throws_Then_Succeeds                        | 13,472.88 ns | 182.343 ns | 170.564 ns | 0.1068 |    1920 B |
| <b>Timeout</b> |
| Timeout_Sync_Succeeds                                     |        142.9 ns |      2.86 ns |      4.86 ns | 0.0381 |     640 B |
| Timeout_Async_Succeeds                                    |        197.4 ns |      3.97 ns |      5.81 ns | 0.0448 |     752 B |
| Timeout_Async_Succeeds_With_CancellationToken             |        184.5 ns |      3.63 ns |      4.59 ns | 0.0448 |     752 B |
| Timeout_Sync_With_Result_Succeeds                         |        126.7 ns |      2.55 ns |      4.11 ns | 0.0329 |     552 B |
| Timeout_Async_With_Result_Succeeds                        |        156.2 ns |      2.96 ns |      3.96 ns | 0.0305 |     512 B |
| Timeout_Async_With_Result_Succeeds_With_CancellationToken |        165.3 ns |      3.27 ns |      5.98 ns | 0.0305 |     512 B |
| Timeout_Async_Times_Out_Optimistic                        | 15,824,382.5 ns | 97,638.60 ns | 91,331.20 ns |      - |    1372 B |
| Timeout_Async_Times_Out_Optimistic                        | 15,824,382.5 ns | 97,638.60 ns | 91,331.20 ns |      - |    1372 B |