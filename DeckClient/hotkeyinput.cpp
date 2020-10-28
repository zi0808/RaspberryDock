#include "hotkeyinput.h"
#include "ui_hotkeyinput.h"

HotkeyInput::HotkeyInput(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::HotkeyInput)
{
    ui->setupUi(this);
}

HotkeyInput::~HotkeyInput()
{
    delete ui;
}

void HotkeyInput::accept()
{
    hotkeystring += std::string(ui->hkmod_ctrl->checkState() ? "^" : "") +
                 (ui->hkmod_alt->checkState() ? "&" : "") +
                (ui->hkmod_shift->checkState() ? "+" : "") +
                ui->text_keys->toPlainText().toStdString();

}
void HotkeyInput::reject()
{

}
