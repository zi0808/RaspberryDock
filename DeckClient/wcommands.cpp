// 2013112501 Yonghyun Kim
// WCommands.cpp Command Handler

#include "wcommands.h"
#include "mainwindow.h"
// Instance Pointer
WCommands * WCommands::Instance = nullptr;
// Constructor
WCommands::WCommands()
{
    if (!Instance)
        Instance = this;
}
// Convert std::string to cstring
char * WCommands::StoCA(string S)
{
    char * converted;
    converted = (char *)malloc(sizeof(char) * (S.length() + 1));
    strcpy(converted, S.c_str());
    return converted;
}
// Parse Sync Command
char * WCommands::WSync()
{
    return StoCA(_APP_SYNC_SIGNAL + " ");
}
// Parse Multimedia Commands
char * WCommands::MCommand(MCNum MultCom)
{
    string Composed = "";
    switch (MultCom) {
    case MC_MVolMute:
        Composed = _CMD_MUL_VOL_MUTE;
        break;
    case MC_MVolDn:
        Composed = _CMD_MUL_VOL_DN;
        break;
    case MC_MVolUp:
        Composed = _CMD_MUL_VOL_UP;
        break;
    case MC_MPBNext:
        Composed = _CMD_MUL_PB_NEXT;
        break;
    case MC_MPBPrev:
        Composed = _CMD_MUL_PB_PREV;
        break;
    case MC_MPBStop:
        Composed = _CMD_MUL_PB_STOP;
        break;
    case MC_MPBPause:
        Composed = _CMD_MUL_PB_PAUSE;
        break;
    }
    Composed += " ";
    return StoCA(Composed);
}
// Parse Launch Commands
char * WCommands::WLaunch(string AppName)
{
    string Composed = _CMD_RUN + " " + AppName;
    return StoCA(Composed);
}
void WCommands::Sync()
{

}
// Parse Win32 Window Commands
char * WCommands::WCommand(string name, WCNum c)
{
    string Composed = "";
    switch (c) {
        case WClose:
            Composed = _CMD_W_CLOSE;
            break;
        case WMax:
            Composed = _CMD_W_MAX;
            break;
        case WMin:
            Composed = _CMD_W_MIN;
            break;
    }
    Composed += " ";
    Composed += name;
    return StoCA(Composed);
}
// Handle Command string received from Server
void WCommands::WRecv(string msg)
{
    int i = 0;
    string code = msg.substr(0,_APP_ADD.length());
    string body;
    if (msg.length() > _APP_ADD.length())
        body = msg.substr(_APP_ADD.length()+1, msg.length() - _APP_ADD.length() - 1);
    if (code == _APP_ADD)
    {
        MainWindow::getMainWinPtr()->EmitAppListUpdate(body, true);
    }
    else if (code == _APP_REM)
        MainWindow::getMainWinPtr()->EmitAppListUpdate(body, false);
    else if (code == _GOODBYE)
    {
        MainWindow::getMainWinPtr()->Kill();
    }
}
// Handle Hotkey
char * WCommands::WHotKey(int index)
{
    string key = hotk_values[index];
    string command = _CMD_HOTKEY;
    command = command + " " + key;
    return StoCA(command);
}
