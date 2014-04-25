IfWinExist, Agent Login
{
    WinActivate
	Send, [PASSCODE]{Enter}
}
else
{
	WinWaitActive Agent Login
	Send, [PASSCODE]{Enter}
}
