IfWinExist Logout - Cisco Agent Desktop
{
    WinActivate
	Send ^l
	WinWaitActive Agent Login
	Send 636363{Enter}
}