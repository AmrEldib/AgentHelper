IfWinExist, Logout - Cisco Agent Desktop
{
	ControlSend, ,^l, Logout - Cisco Agent Desktop
	WinWait, Agent Login, , 4
	WinActivate, Agent Login
	Send [PASSCODE]{Enter}
	if ErrorLevel
	{
		MsgBox, Couldn't Log in
		return
	}
}
