IfWinExist, Logout - Cisco Agent Desktop
{
    WinActivate
	Send, ^l
	WinWaitActive, Agent Login
	Send, [PASSCODE]{Enter}
}