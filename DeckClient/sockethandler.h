#ifndef SOCKETHANDLER_H
#define SOCKETHANDLER_H
// =======================================
// 2013112501 김용현 Yonghyun Kim
// =======================================
// Deck Client Header ( Socket Handling Class )
// =======================================
#include <QObject>
#include <QtDebug>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <netinet/tcp.h>
#include <string.h>
#include <netinet/in.h>
#include <fcntl.h>
#include <string>
#include <thread>
#define BUFFER_SIZE 1500
class SocketHandler
{
private:
    sockaddr_in sock_sv;
public:
    SocketHandler();
    const int port = 8000;
    int Connect();
    void Disconnect();
    void StartRecv();
    int SendMsg(char * msg);
    static std::vector<std::string> inQueue;
    static char InChar[BUFFER_SIZE];
    std::string ConnAddrStr;
    std::thread thr_read;
    bool IsIPValid();
    static bool IsThreadReading;
    static int sockfd;
    static int thread_id;

    // Depreacted Functions
    /*
    void RecvLoop();
    int RecvMsg();
    */
};

#endif // SOCKETHANDLER_H
