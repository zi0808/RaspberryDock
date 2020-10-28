#ifndef HOTKEYINPUT_H
#define HOTKEYINPUT_H

#include <QDialog>
#include <QPlainTextEdit>
#include <QCheckBox>
#include <QButtonGroup>
namespace Ui {
class HotkeyInput;
}

class HotkeyInput : public QDialog
{
    Q_OBJECT

public:
    explicit HotkeyInput(QWidget *parent = nullptr);
    ~HotkeyInput();

private:
    Ui::HotkeyInput *ui;
    std::string hotkeystring;
public slots:
    void accept() override;
    void reject() override;
};

#endif // HOTKEYINPUT_H
