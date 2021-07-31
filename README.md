# BoidSimulation_URP
[![CI_SONAR_CLOUD](https://github.com/eugene-doobu/BoidSimulation_URP/actions/workflows/sonarcloud-analysis.yml/badge.svg)](https://github.com/eugene-doobu/BoidSimulation_URP/actions/workflows/sonarcloud-analysis.yml)
[![unittest](https://github.com/eugene-doobu/BoidSimulation_URP/actions/workflows/unittest.yml/badge.svg)](https://github.com/eugene-doobu/BoidSimulation_URP/actions/workflows/unittest.yml)
</br>
 2021 방통대 소프트웨어 경진대회를 위한 군집 시뮬레이션 테스트

## Outline
GIF
GPU 병렬 연산을 통한 군집 시뮬레이션을 수행하는 프로젝트입니다.


## Performance Report
- CPU: AMD Ryzen 9 3900x 12-core cpu
- SSD: Samaung 980 PRO PCIe 4.0
- GPU: NVIDIA GeForce RTX 2080 SUPER
- DirectX12
</br></br>

이 프로젝트는 <strong>컴퓨트 셰이더</strong>를 핵심기술로 사용합니다. 컴퓨트 셰이더가 작동하는 플랫폼은 다음과 같습니다.
- DirectX 11 또는 DirectX 12 그래픽스 API와 Shader Model 5.0 GPU가 적용된 Windows 및 Windows 스토어
- Metal 그래픽스 API를 사용하는 Mac OS 및 iOS
- Vulkan API가 적용된 Android, Linux 및 Windows 플랫폼
- 최신 OpenGL 플랫폼( Linux 또는 Windows의 경우 OpenGL 4.3, Android의 경우 OpenGL ES 3.1). Mac OS X는 OpenGL 4.3을 지원하지 않습니다.
- 최신 콘솔(Sony PS4 및 Microsoft Xbox One)
</br></br>

## Contact  
<a href="https://www.youtube.com/channel/UCsvrVhm_WRjNVOtoRrk0-hA/"><img src="https://img.shields.io/badge/YouTube-FF0000?style=flat-square&logo=YouTube&logoColor=white&link=https://www.youtube.com/channel/UCsvrVhm_WRjNVOtoRrk0-hA/"/></a>
</br></br>

## included packages
깃허브 저장소 프로젝트에 포함되어있는 패키지와 에셋들입니다.

- <a href="https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676">Dotween(Free ver)</a>
- <a href="https://assetstore.unity.com/packages/tools/integration/unirx-reactive-extensions-for-unity-17276">UniRx</a>
- <a href="https://github.com/Unity-Technologies/EntityComponentSystemSamples">EntityComponentSystemSamples(model만 사용)</a>
- <a href="https://github.com/unity-korea-community/unity-builder.git">Unity-Builder</a>
- <a href="https://assetstore.unity.com/packages/tools/camera/cameracontroller-141370">D2og - CameraController</a>
</br></br>

## Asset Store Assets</br>
유니티 에셋스토어에서 받은 에셋들은 깃허브 저장소에 포함되지 않도록 설정하였습니다. 이 프로젝트를 다운받아 영상과 같은 테스트를 하기 위해서는 아래 에셋들이 필요합니다.

- <a href="https://assetstore.unity.com/packages/audio/ambient/underwater-ambient-66498">Underwater Ambient</a>
- <a href="https://jkhub.org/files/file/3216-underwater-skybox/">Underwater Skybox</a>
</br></br>

## Reference</br>
<a href="https://github.com/IndieVisualLab/UnityGraphicsProgramming">UnityGraphicsProgramming</a></br>
- 第3章 群のシミュレーションのGPU実装
- hiroakioishi: https://irishoak.tumblr.com/</br></br>

Flocks, Herds, and Schools:A Distributed Behavioral Model
- ACM SIGGRAPH '87 Conference Proceedings, Anaheim, California, July 1987
- Craig W. Reynolds</br></br>

Perception of Realistic Flocking Behavior in the Boid Algorithm
- Bachelor Thesis Computer Science 10 2017
- Max Larsson and Sebastian Lundgren</br></br>

Fish Swimming Animation
- https://github.com/albertomelladoc/Fish-Animation/blob/master/FishAnimation.shader
- https://www.bitshiftprogrammer.com/2018/01/how-to-animate-fish-swimming-with.html</br></br>

Volumetric Lighting
- https://github.com/Unity-Technologies/VolumetricLighting</br></br>

<a href="https://catlikecoding.com/unity/tutorials/">Catlike Coding Unity Tutorials</a></br>
- Basic</br></br>

## License
### Unity Companion License ("License")</br>
Entity Component System copyright © 2017-2020 Unity Technologies ApS

Licensed under the Unity Companion License for Unity-dependent projects--see <a href="https://unity3d.com/legal/licenses/Unity_Companion_License">Unity Companion License.</a>

Unless expressly provided otherwise, the Software under this license is made available strictly on an “AS IS” BASIS WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED. Please review the license for details on these and other terms and conditions.
</br></br>
