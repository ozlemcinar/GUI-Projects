#pragma once



#ifndef __AFXWIN_H__

#error "include 'stdafx.h' before including this file for PCH"

#endif



#include "resource.h" 


class Cproject3 : public CWinApp

{

public:

	Cproject3();

public:

	virtual BOOL InitInstance();

	virtual int ExitInstance();



public:

	afx_msg void OnAppAbout();

	DECLARE_MESSAGE_MAP()

};



extern Cproject3 theApp;