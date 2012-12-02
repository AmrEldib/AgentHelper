IfWinExist Logout - Cisco Agent Desktop
{
    WinActivate
	Send !{F4}
}
IfWinExist Ready - Cisco Agent Desktop
{
    WinActivate
	// Log out
	Send !{F4}
}
IfWinExist Not Ready - Cisco Agent Desktop
{
    WinActivate
	// Log out
	Send !{F4}
}