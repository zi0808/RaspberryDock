# RaspberryDock
Raspberry Pi Qt / Windows .Net Application, Lets you control Windows PC with Pi.
This was a final project for a university lecture.
 
# What can it do?


# Why does this exist?
So, i have to do a project using Pi and Qt either way. And one thing that came to my mind was **Elgato Stream Deck**.
And the RPi i was given, had a touch screen as well. Technically, that means both of them can function similarly,
If i just add application that can send commands to the PC.

# Why use TCP, When you have Serial?
I initially thought of using Serial, Since that would mean the RPi can be just pluuged onto the PC for application.
However, one of the requirements were indeed, use of TCP sockets.

# How to Test / Demo this code
You will need :
 - Raspberry Pi 3B or higher, With LAN or WLAN connetivity. ( inteded to use with Touch Screen Chassis )
 - a Linux PC or Windows PC with WSL enabled, with **Ubuntu and Qt Creator** for cross-compiling.
 - a Windows PC.

 - "DeckServer" is the TCP Listener on Windows.
 - "DeckClient" is the TCP Client on RPi.
