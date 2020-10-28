# RaspberryDock [라즈베리 독]

Raspberry Pi Qt / Windows .Net Application, Lets you control Windows PC with Pi.
This was a final project for a university lecture.

라즈베리 파이를 이용한 Qt (C++) / Windows .Net (C#) 앱입니다.
라즈베리 파이를 이용하여, Windows PC 를 제어할수 있습니다.
이 프로젝트는 대학 강의의 기말 프로젝트를 목적으로 제작되었습니다.
 
## What can it do? [이걸로 뭘 할 수 있죠?]
![Screenshot.png]
- Currently, all actions are pre-defined<br>
  현재는 모든 기능이 미리 정의된것만 사용됩니다
- Pass hotkey input to Windows.<br>
  윈도우로 단축키 명령을 보낼 수 있습니다.<br>
- Close, Minimize, or Maximize applications.
  열려있는 어플리케이션 창을 닫거나, 최소화/최대화 할수 있습니다.<br>
- Media Control : Volume, Track Change, Pause/Play<br>
  미디어 컨트롤 : 볼륨 조절, 트랙 변경, 재생/일시정지
- Launch Apps : Chrome, and websites on Chrome, Explorer, Visual Studio, CMD.<br>
  어플리케이션 실행 : 크롬, 웹사이트 지정해 크롬 열기, 탐색기, 비주얼 슈튜디오, 명령 프롬프트

## Why does this exist? [왜 만드셨어요?]
So, i have to do a project using Pi and Qt either way. And one thing that came to my mind was **Elgato Stream Deck**.
And the RPi i was given, had a touch screen as well. Technically, that means both of them can function similarly,
If i just add application that can send commands to the PC.<br>
프로젝트를 하면서 어찌 되었든 라즈베리 파이랑 Qt 가 요구 사항이였는데, **엘가토 스트림 덱** 이 생각이 났습니다.
프로젝트용으로 대여한 라즈베리 파이에 터치 스크린도 있었고, 여기에 적절하게 프로그램만 얹어준다면
비슷하게 PC 에 명령어를 보낼수 있겠다는 생각이 들더군요.

## Why use TCP, When you have Serial? [시리얼 놥두고 왜 TCP 쓰셨죠?]
I initially thought of using Serial, Since that would mean the RPi can be just pluuged onto the PC for application.
However, one of the requirements were indeed, use of TCP sockets.<br>
당연하지만 처음엔 시리얼 포트를 사용하는걸 생각했습니다. 그렇게 되면 그냥 컴퓨터에 USB-시리얼로 장착해서 쓸수 있으니 말이죠.
하지만 프로젝트 요구사항이 TCP 사용이므로 그렇게 됬습니다.

## How to Test / Demo this code [코드 테스트/실행해보기]
You will need :
 - Raspberry Pi 3B or higher, With LAN or WLAN connetivity. ( inteded to use with Touch Screen Chassis )
 - a Linux PC or Windows PC with WSL enabled, with **Ubuntu and Qt Creator** for cross-compiling.
 - a Windows PC.

 - "DeckServer" is the TCP Listener on Windows.
 - "DeckClient" is the TCP Client on RPi.
<br>
필요한 사항은 다음과 같습니다 :
 - 라즈베리 파이 3B 또는 그 이후 버전, 유선 또는 무선 LAN 연결 ( 터치스크린 케이스 사용을 권장합니다. )
 - 리눅스 PC 또는 WSL 이 설치된 Windows PC. 두 경우 모두 **우분투 / Qt 크리에이터** 가 있어야 크로스 컴파일이 가능합니다.
 - Windows 설치된 PC.
 
 - "DeckServer" 는 윈도우에서 실행하는 TCP 리스너입니다.
 - "DeckClient" 는 라즈베리 파이에서 실행하는 TCP 클라이언트 입니다.

## Disclaimer on Usage [사용 조건]
 - You are allowed to use, modify this code for anything. However if you do, It would be great if you mention this repo.<br>
   어떤 목적으로든 코드를 사용하거나 편집해도 좋습니다. 단 사용하실 경우 이 저장소 링크를 남겨주시면 감사하겠습니다.
 - However, this is more of demo / showcase, and i do not plan to make further improvements,
   Thus, i am probably also not answering any questions. Feel free to fork the repo and make improvements yourself.<br>
   데모용에 가까운 코드이기때문에, 앞으로 개선하거나 유지관리 하진 않으며,
   질문은 받지 않겠습니다. fork 하셔서 직접 개선해 보시는것도 좋을 것 같습니다.
