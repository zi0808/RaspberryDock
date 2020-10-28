#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QPushButton>
#include <QTextEdit>
#include <QMessageBox>
#include <QGroupBox>
#include "sockethandler.h"
#include "wcommands.h"
#include "hotkeyinput.h"
namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    void StatUpdate(const char * stat);
    ~MainWindow();
    SocketHandler * SHandle;
    WCommands * WCommand;
    static MainWindow * getMainWinPtr();
    void EmitAppListUpdate(string name, bool add);
    void Kill();
private:
    pid_t pid;
    static MainWindow * pMainWindow;
    std::vector<QString> vec_qstr;
    Ui::MainWindow *ui;
    MCNum MCV;
    QMessageBox err;
    QMessageBox InputHotkey;
    bool CustomizeMode = false;
    int appindex;
    const QString sockerr_conn = "-2 : Connection Error";
    const QString sockerr_creat = "-1 : Socket Creation Error";
    const QString sock_nc = "Not Connected";
    const QString sock_conn = "Connected";
    const QString sock_try = "Connecting..";
    const QString cs_mode_user = "Customize";
    const QString cs_mode_cust = "Finalize";
    void Handle_MultComm(MCNum C);
    void HandleHotkey(int index);
    void HandleRunCustom(int index);
    void ControlsEnable(bool enable);
    HotkeyInput HKDialog;
signals:
    void AppListVSend(string S, bool add);
public slots:
    void Handle_ConnectBtn();
    void HMMute();
    void HMVolUp();
    void HMVolDn();
    void HMNextTrack();
    void HMPrevTrack();
    void HMPause();
    void HAddressInput();
    void RunChrome();
    void RunCMD();
    void RunCustom1() {HandleRunCustom(0);}
    void RunCustom2() {HandleRunCustom(1);}
    void RunCustom3() {HandleRunCustom(2);}
    void RunCustom4() {HandleRunCustom(3);}
    void RunCustom5() {HandleRunCustom(4);}
    void RunCustom6() {HandleRunCustom(5);}
    void AppClose();
    void AppMaximize();
    void AppMinimize();
    void AppIndexChange(int index);
    void AppListUpdate(string name, bool add = true);
    void HotkeyUndo();
    void Hotkey1() { HandleHotkey(0);}
    void Hotkey2(){ HandleHotkey(1);}
    void Hotkey3(){ HandleHotkey(2);}
    void Hotkey4(){ HandleHotkey(3);}
    void Hotkey5(){ HandleHotkey(4);}
    void Hotkey6(){ HandleHotkey(5);}
    void Hotkey7(){ HandleHotkey(6);}
    void CustomizeToggle();
};
//MainWindow * MainWindow::Instance = nullptr;
#endif // MAINWINDOW_H
