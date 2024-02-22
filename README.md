# 아쿠아리움

## 🎉프로젝트 소개
Visual Studio와 Unity 를 이용해서 C#으로 제작한 프로그램.

### 개발자
- 신은지

## 게임 컨셉
힐링주차로 아쿠아리움을 만들고 싶었다.
<br>

## 📅개발 기간
* 2024년 2월 21일 ~ 2024년 2월 22일


## 🏟️개발 환경
* Visual Studio 2022
* C#
* .NET 8.0
* Windows
* Nugat 패키지
* Unity

## 주요기능
### Player 이동

* InputAction 사용
* FSM 이용

#### 상태머신.cs
* <IState> 플레이어의 현 상태 인터페이스
* <PlayerBaseState> IState를 정의 하는 부모 클래스
* <PlayerInputAction> 플레이어의 InputAction 환경을 담은 스크립트
* <PlayerN> 플레이어의 데이터 생성, 상태머신을 가동하는 스크립트
* <StateMachine> 상태 전환 상위 클래스
* <PlayerStateMachine> 플레이어의 상태 전환하는 스크립트

#### 상태.cs

PlayerGroundedState 상속 : 땅위에서의 상태
- PlayerIdleState : 가장 기본 상태
- PlayerWalkState : 움직이는 상태
- PlayerJumpState : 점프 상태

PlayerAirState : 허공에서의 상태


### Fish 이동
* Flock 알고리즘 사용

- Flock: 물고기 생성과 물고기전체를 관리하는 스크립트
- FishController: 물고기 개개인의 움직임을 관리하는 스크립트
- AvoidLayer: 특정 레이어를 가진 오브젝트를 피하는 힘을 주는 스크립트


### 씬 이동
- SceneLoad : 씬을 이동하는 메소드가 있는 스크립트


### 사용에셋 스크립트
- Fish 프리팹 4종
- Glass 프리팹
- Fish Bord 스크립트
- Sky 머트리얼
- Ligth 오브젝트
- Ground 텍스쳐


