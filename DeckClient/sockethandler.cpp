// 2013112501 Yonyghyun Kim
// Socket Handler

#include "sockethandler.h"
#include "wcommands.h"
#include "mainwindow.h"

char SocketHandler::InChar[BUFFER_SIZE];
int SocketHandler::sockfd;
int SocketHandler::thread_id;
std::vector<std::string> SocketHandler::inQueue;
bool SocketHandler::IsThreadReading;

SocketHandler::SocketHandler()
{

}

int SocketHandler::Connect()
{
    fd_set myset;
    struct timeval tv;
    socklen_t lon;
    int arg, valopt;
    if ((sockfd = socket(AF_INET, SOCK_STREAM, 0)) < 0)
        return -1;
    bzero(&sock_sv, sizeof(sock_sv));
    sock_sv.sin_family = AF_INET;
    sock_sv.sin_addr.s_addr = inet_addr(ConnAddrStr.c_str());
    sock_sv.sin_port = htons(port);
    // Set Socket to Non-Blocking
    if (fcntl(sockfd, F_SETFL, O_NONBLOCK) < 0)
        return -3;
    int res = connect(sockfd,(struct sockaddr*)&sock_sv, sizeof(sock_sv));
    if (res < 0)
    {
        if (errno == EINPROGRESS) {
            do {
                tv.tv_sec = 15;
                tv.tv_usec = 0;
                FD_ZERO(&myset);
                FD_SET(sockfd, &myset);
                res = select(sockfd+1,NULL,&myset,NULL,&tv);
                if (res < 0 && errno != EINTR)
                    return -4;
                else if(res > 0)
                {
                    lon = sizeof(int);
                    if (getsockopt(sockfd, SOL_SOCKET, SO_ERROR, (void*)(&valopt),&lon) < 0)
                        return -5;
                    if (valopt)
                        return -6;
                    break;
                }
                else {
                    return -7;
                }
            }while(1);
        }
        else
            return -1;
    }
    return 0;
}
// Send out Messages
int SocketHandler::SendMsg(char * msg)
{
    if (sockfd < 0)
        return -1;
    return send(sockfd, msg, strlen(msg), MSG_WAITFORONE);
}
// Threaded Receive
static void thrRecv()
{
    while (SocketHandler::sockfd >= 0)
    {
        bzero(SocketHandler::InChar, BUFFER_SIZE);
        int res = recv(SocketHandler::sockfd, SocketHandler::InChar, BUFFER_SIZE, MSG_WAITFORONE);
        if (res > 0)
        {
            SocketHandler::IsThreadReading = true;
            string s = string(SocketHandler::InChar);
            WCommands::Instance->WRecv(s);
        }
        else
        {
            SocketHandler::IsThreadReading = false;
        }
    }
    SocketHandler::IsThreadReading = false;
    close(SocketHandler::sockfd);
    MainWindow::getMainWinPtr()->close();
}
// Start Receiving thread
void SocketHandler::StartRecv()
{
    thr_read = thread(thrRecv);
}
// Disconnect From Server
void SocketHandler::Disconnect()
{
    SendMsg("byebye ");
    close(sockfd);
}
// Check IPv4 validity
bool SocketHandler::IsIPValid()
{
    struct sockaddr_in sa;
    int result = inet_pton(AF_INET, ConnAddrStr.c_str(), &(sa.sin_addr));
    return result != 0;
}

// Deprecated Functions
/*
// (Deprecated) Receive One
int SocketHandler::RecvMsg()
{
    bzero(InChar, BUFFER_SIZE);
    if (sockfd < 0)
        return -1;
    int res = recv(sockfd, InChar, BUFFER_SIZE, 0);
    std::string Msg("Received ");
    Msg += std::to_string(res) + " Bytes";
    MainWindow::getMainWinPtr()->StatUpdate(Msg.c_str());
    SocketHandler::inStr.assign(InChar, strlen(InChar));
    if (res > 0)
        WCommands::Instance->WRecv(InChar);
    return res;
}
// (Deprecated) Receive Loop
void SocketHandler::RecvLoop()
{
    while (sockfd >= 0)
    {
        int res = RecvMsg();
        if (res > 0)
        {
            if (strcmp(InChar,"appsync_e ") == 0)
                break;
        }
        else
        {
            MainWindow::getMainWinPtr()->StatUpdate("No Input");
        }
    }
}
*/
