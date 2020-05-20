#include "stdafx.h"

#include "afxwinappex.h"

#include "afxdialogex.h"

#include "ass3.h"

#include "MainFrm.h"





#ifdef _DEBUG

#define new DEBUG_NEW

#endif



BEGIN_MESSAGE_MAP(Cproject3, CWinApp)

	ON_COMMAND(ID_APP_ABOUT, &Cproject3::OnAppAbout)

END_MESSAGE_MAP()


Cproject3::Cproject3()

{
	m_dwRestartManagerSupportFlags = AFX_RESTART_MANAGER_SUPPORT_ALL_ASPECTS;

#ifdef _MANAGED

	System::Windows::Forms::Application::SetUnhandledExceptionMode(System::Windows::Forms::UnhandledExceptionMode::ThrowException);

#endif


	SetAppID(_T("cs360Asgn2.AppID.NoVersion"));


}


Cproject3 theApp;


BOOL Cproject3::InitInstance()

{
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);
	CWinApp::InitInstance();
	if (!AfxOleInit())

	{
		return FALSE;
	}
	AfxEnableControlContainer();
	EnableTaskbarInteraction(FALSE);

	SetRegistryKey(_T("Hello from Ozlem Cinar"));
	CMainFrame* pFrame = new CMainFrame;

	if (!pFrame)

		return FALSE;

	m_pMainWnd = pFrame;
	pFrame->LoadFrame(IDR_MAINFRAME,

		WS_OVERLAPPEDWINDOW | FWS_ADDTOTITLE, NULL,

		NULL);
	pFrame->ShowWindow(SW_SHOW);

	pFrame->UpdateWindow();

	return TRUE;

}



int Cproject3::ExitInstance()

{

	AfxOleTerm(FALSE);



	return CWinApp::ExitInstance();

}
class CAboutDlg : public CDialogEx

{

public:

	CAboutDlg();


#ifdef AFX_DESIGN_TIME

	enum { IDD = IDD_ABOUTBOX };

#endif



protected:

	virtual void DoDataExchange(CDataExchange* pDX);

protected:

	DECLARE_MESSAGE_MAP()

};



CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)

{

}



void CAboutDlg::DoDataExchange(CDataExchange* pDX)

{

	CDialogEx::DoDataExchange(pDX);

}



BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)

	ON_WM_CREATE()

	ON_WM_SHOWWINDOW()

	ON_WM_ACTIVATE()

	ON_WM_MOVE()

	ON_WM_PAINT()

	ON_WM_SETFOCUS()

END_MESSAGE_MAP()
void Cproject3::OnAppAbout()

{

	CAboutDlg aboutDlg;
	aboutDlg.DoModal();

}