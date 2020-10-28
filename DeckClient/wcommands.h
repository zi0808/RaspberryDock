#ifndef WCOMMANDS_H
#define WCOMMANDS_H
// =======================================
// 2013112501 김용현 Yonghyun Kim
// =======================================
// Deck Client Header ( Command Composer )
// =======================================
#include <string>
#include <sstream>
#include <iostream>
#include <map>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
using namespace std;
enum WCNum
{
    WClose,
    WMax,
    WMin,
};
enum MCNum
{
    MC_MVolUp,
    MC_MVolDn,
    MC_MVolMute,
    MC_MPBPause,
    MC_MPBNext,
    MC_MPBPrev,
    MC_MPBStop,
    MC_Hotkey,
};
class WCommands
{
public:
    static WCommands * Instance;
    WCommands();
   // Public Constants
    // Application Specific
    const string _APP_ADD = "appadd";
    const string _APP_REM = "apprem";
    const string _APP_SYNC_SIGNAL = "appsync";
    const string _APP_SYNC_END = "appsync_e ";
    const string _GOODBYE = "byebye";
   // Win32 OS Commands ( Op and Arg )
    const string _CMD_W_CLOSE = "wclose";
    const string _CMD_W_MAX = "wmax";
    const string _CMD_W_MIN = "wmin";
    const string _CMD_RUN = "run";
    // Multimedia Commands ( Op Only )
    const string _CMD_MUL_VOL_UP = "mvup";
    const string _CMD_MUL_VOL_DN = "mvdn";
    const string _CMD_MUL_VOL_MUTE = "mmute";
    const string _CMD_MUL_PB_PAUSE = "mpbpause";
    const string _CMD_MUL_PB_NEXT = "mpbnxt";
    const string _CMD_MUL_PB_PREV = "mpbprev";
    const string _CMD_MUL_PB_STOP = "mpbstop";
    const string hotk_values[6] = {"^Y","&{TAB}","&+S","^C","^P","^Z"};
     // Hotkey Command ( Op And Arg(s) )
    const string _CMD_HOTKEY = "hotk";
    char * WCommand(string Name, WCNum wNum);
    char * WLaunch(string AppName);
    char * MCommand(MCNum MultCom);
    void Sync();
    void WRecv(string msg);
    char * WSync();
    char * WHotkey(string * HotKeys);
    char * StoCA(string S);
    char * WHotKey(int index);
    FILE * ConfigFile;
    const string _NO_CONFIG = "N/A";
};

#endif // WCOMMANDS_H
