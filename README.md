# DOTS Unity Shooter

First thing that I'd like to mention is that the hardest part was figuring out what is compatible with the [BurstCompile] and what isn't, a few scripts were giving me issues and therefore disabling me from building the project into a windows executable that would
run without errors. After removing the [BurstCompile] from where it wasn't compatible with the code, my project built correctly and
is still running on high FPS since the [BurstCompile] is working where it matters most, which is for the bullet instantiation. 

Despite using DOTS, the entire system as a whole was still not that much performant over OOP until the BurstCompiler was functional
with the bullet instantiation. In summary, creating code that is compatible with BurstCompiling is the most efficient way to get an FPS increase when using Data Oriented Technology Stack. 

Jobs were implemented in order to correctly utilize multithreading to improve performance as it is one of the few primary ways that DOTS is able to grant more optimal performance over OOP. 

Using partial structs has also granted a small increase in performance, all of which were tested using profiler in Unity. 