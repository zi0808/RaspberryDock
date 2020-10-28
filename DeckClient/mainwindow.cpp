#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow * MainWindow::pMainWindow = nullptr;
MainWindow *MainWindow::getMainWinPtr()
{
    return pMainWindow;
}

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    qRegisterMetaType<string>("string");
    qRegisterMetaType<bool>("bool");
    SHandle = new SocketHandler ();
    WCommand = new WCommands();
    ui->setupUi(this);
    connect(ui->btnVolMute, SIGNAL(clicked()), this, SLOT(HMMute()));
    connect(ui->BtnConn, SIGNAL(clicked()), this, SLOT(Handle_ConnectBtn()));
    connect(ui->btnVoldn, SIGNAL(clicked()), this, SLOT(HMVolDn()));
    connect(ui->btnVolup, SIGNAL(clicked()), this, SLOT(HMVolUp()));
    connect(ui->btnPause, SIGNAL(clicked()), this, SLOT(HMPause()));
    connect(ui->btnNextTrack, SIGNAL(clicked()), this, SLOT(HMNextTrack()));
    connect(ui->btnPrevTrack, SIGNAL(clicked()), this, SLOT(HMPrevTrack()));
    connect(ui->AddressField, SIGNAL(textChanged()), this, SLOT(HAddressInput()));
    connect(ui->btn_cmd, SIGNAL(clicked()), this, SLOT(RunCMD()));
    connect(ui->btn_chrome, SIGNAL(clicked()), this, SLOT(RunChrome()));
    connect(ui->btn_appclose, SIGNAL(clicked()),this, SLOT(AppClose()));
    connect(ui->btn_appmax, SIGNAL(clicked()),this, SLOT(AppMaximize()));
    connect(ui->btn_appmin, SIGNAL(clicked()),this,SLOT(AppMinimize()));
    connect(ui->AppList, SIGNAL(currentRowChanged(int)),this,SLOT(AppIndexChange(int)));
    connect(this, SIGNAL(AppListVSend(string,bool)), this, SLOT(AppListUpdate(string,bool)));
    connect(ui->btn_undo, SIGNAL(clicked()),this, SLOT(HotkeyUndo()));
    connect(ui->btn_hotk1, SIGNAL(clicked()), this, SLOT(Hotkey1()));
    connect(ui->btn_hotk2, SIGNAL(clicked()), this, SLOT(Hotkey2()));
    connect(ui->btn_hotk3, SIGNAL(clicked()), this, SLOT(Hotkey3()));
    connect(ui->btn_hotk4, SIGNAL(clicked()), this, SLOT(Hotkey4()));
    connect(ui->btn_hotk5, SIGNAL(clicked()), this, SLOT(Hotkey5()));
    connect(ui->btn_run_c1, SIGNAL(clicked()), this, SLOT(RunCustom1()));
    connect(ui->btn_run_c2, SIGNAL(clicked()), this, SLOT(RunCustom2()));
    connect(ui->btn_run_c3, SIGNAL(clicked()), this, SLOT(RunCustom3()));
    connect(ui->btn_run_c4, SIGNAL(clicked()), this, SLOT(RunCustom4()));
    connect(ui->btn_run_c5, SIGNAL(clicked()), this, SLOT(RunCustom5()));
    connect(ui->btn_run_c6, SIGNAL(clicked()), this, SLOT(RunCustom6()));
    connect(ui->btnCustomize, SIGNAL(clicked()),this,SLOT(CustomizeToggle()));
    //connect(ui->btn_run_c5, SIGNAL(clicked()), this, SLOT(RunCustom5()));
    //connect(ui->btn_hotk6, SIGNAL(clicked()), this, SLOT(Hotkey6()));
    ControlsEnable(false);
    pMainWindow = this;
}

void MainWindow::ControlsEnable(bool enable)
{
    ui->WinIcon->setEnabled(enable);
    ui->Group_App->setEnabled(enable);
    //ui->Group_Hotkeys->setEnabled(enable);
    ui->Group_AppLaunch->setEnabled(enable);
    ui->Group_MediaControl->setEnabled(enable);
}

MainWindow::~MainWindow()
{
    SHandle->Disconnect();
    delete ui;
}
void MainWindow::HotkeyUndo()
{
    SHandle->SendMsg(WCommand->WHotKey(5));
}

void MainWindow::HandleHotkey(int index)
{
    SHandle->SendMsg(WCommand->WHotKey(index));
}

void MainWindow::Kill()
{
    SHandle->Disconnect();
    ui->BtnConn->setText("Connect");
    ControlsEnable(false);
    close();
}

void MainWindow::Handle_ConnectBtn()
{
    if (SHandle->sockfd > 0)
    {
        Kill();
    }
    else
    {
        // Try Connecting
        if (!SHandle->IsIPValid())
        {
            err.setText("That is not correct IPv4 Address.");
            err.setInformativeText("IPv4 Format is xxx.xxx.xxx.xxx");
            err.setStandardButtons(QMessageBox::Ok);
            err.setDefaultButton(QMessageBox::Ok);
            err.exec();
            return;
            // Return if IP is not valid
        }
        ui->BtnConn->setText("Connecting");
        int sockresult = SHandle->Connect();
        if (sockresult < 0)
            ui->statusBar->showMessage("Connection Failed.");
        else
        {
            ui->statusBar->showMessage(sock_conn + new QString(SHandle->ConnAddrStr.c_str()));
            ui->BtnConn->setText("Disconnect");
            ui->AddressField->setEnabled(false);
            ControlsEnable(true);

            SHandle->SendMsg(WCommand->WSync());
            SHandle->StartRecv();
        }
    }
}

void MainWindow::StatUpdate(const char *stat)
{
    ui->statusBar->showMessage(stat);
}

void MainWindow::AppListUpdate(string name, bool add)
{
    QString newStr = QString::fromStdString(name);
    if (add)
    {
        vec_qstr.push_back(newStr);
        ui->AppList->addItem(newStr);
        ui->AppList->update();
        //ui->AppList->addItem(&newStr);
    }
    else
    {
        std::vector<QString>::iterator itr = std::find(vec_qstr.begin(),
                                                       vec_qstr.end(), newStr);
        if (itr != vec_qstr.end())
            ui->AppList->takeItem(itr->indexOf(newStr));

        //ui->AppList-
        //ui->AppList->update();
        //ui->AppList->takeItem()
    }
}

void MainWindow::HMMute()
{
    Handle_MultComm(MC_MVolMute);
}

void MainWindow::HMPause()
{
    Handle_MultComm(MC_MPBPause);
}

void MainWindow::HMVolDn()
{
    Handle_MultComm(MC_MVolDn);
}

void MainWindow::HMVolUp()
{
    Handle_MultComm(MC_MVolUp);
}

void MainWindow::HMNextTrack()
{
    Handle_MultComm(MC_MPBNext);
}

void MainWindow::HMPrevTrack()
{
    Handle_MultComm(MC_MPBPrev);
}

void MainWindow::Handle_MultComm(MCNum C)
{
    char * command = WCommand->MCommand(C);
    SHandle->SendMsg(command);
}

void MainWindow::RunChrome()
{
    char * command = WCommand->WLaunch("chrome");
    SHandle->SendMsg(command);
}

void MainWindow::RunCMD()
{
    char * command = WCommand->WLaunch("cmd");
    SHandle->SendMsg(command);
}

void MainWindow::HAddressInput()
{
    // Empty for now
    QString QS = ui->AddressField->toPlainText();
    SHandle->ConnAddrStr = QS.toStdString();
}
void MainWindow::AppClose()
{
    string Current = ui->AppList->item(appindex)->text().toStdString();
    char * commands = WCommand->WCommand(Current, WClose);
    SHandle->SendMsg(commands);
    ui->AppList->takeItem(appindex);
}
void MainWindow::AppMaximize()
{
    string Current = ui->AppList->item(appindex)->text().toStdString();
    char * commands = WCommand->WCommand(Current, WMax);
    SHandle->SendMsg(commands);
}
void MainWindow::AppMinimize()
{
    string Current = ui->AppList->item(appindex)->text().toStdString();
    char * commands = WCommand->WCommand(Current, WMin);
    SHandle->SendMsg(commands);
}
void MainWindow::AppIndexChange(int index)
{
    appindex = index;
}
void MainWindow::HandleRunCustom(int index)
{
    char * command;
    switch (index) {
    case 0:
        command = WCommand->WLaunch("chrome youtube.com");
        break;
    case 1:
        command = WCommand->WLaunch("chrome facebook.com");
        break;
    case 2:
        command = WCommand->WLaunch("chrome twitter.com");
        break;
    case 3:
        command = WCommand->WLaunch("chrome linkedin.com");
        break;
    case 4:
        command = WCommand->WLaunch("explorer");
        break;
    case 5:
        command = WCommand->WLaunch("devenv");
        break;
    }
    SHandle->SendMsg(command);
}
void MainWindow::EmitAppListUpdate(string name, bool add)
{
    emit AppListVSend(name,add);
}
void MainWindow::CustomizeToggle()
{
    //CustomizeMode = !CustomizeMode;
    //ui->btnCustomize->setText(CustomizeMode ? cs_mode_cust : cs_mode_user);
}
