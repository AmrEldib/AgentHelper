IfWinExist Ready - Cisco Agent Desktop
{
    WinActivate
	Send ^l
}
else
{
	IfWinExist Not Ready - Cisco Agent Desktop
	{
		WinActivate
		Send ^l
	}
}