#include "stdafx.h"
#include "ass3.h"
#include <iostream>
#include "MainFrm.h"

#ifdef _DEBUG

#define new DEBUG_NEW

#endif

int globalCounter = 0;

IMPLEMENT_DYNAMIC(CMainFrame, CFrameWnd)

const int iMaxUserToolbars = 10;
const UINT uiFirstUserToolBarId = AFX_IDW_CONTROLBAR_FIRST + 40;
const UINT uiLastUserToolBarId = uiFirstUserToolBarId + iMaxUserToolbars - 1;

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)

	ON_WM_PAINT()

	ON_WM_MOVE()

END_MESSAGE_MAP()



static UINT indicators[] =

{

	ID_SEPARATOR,

	ID_INDICATOR_CAPS,

	ID_INDICATOR_NUM,

	ID_INDICATOR_SCRL,

};

CMainFrame::CMainFrame(){

}

void CMainFrame::OnPaint(){

	CDC * dc; dc = GetDC();
	CPaintDC dc2(this);
	CRect clientR, windowR;
	GetWindowRect(&windowR);
	GetClientRect(&clientR);

	CString message = _T("Hello from Ozlem Cinar");
	dc2.TextOut(0, 0, message);

	if (globalCounter > 1) {

		dc->SetTextColor(RGB(136, 168, 254));

	} globalCounter++;



	CString coordinates;
	coordinates.Format(_T("Left = %d, Right = %d, Top = %d, Bottom = %d"), windowR.left, windowR.right, windowR.top, windowR.bottom);
	dc->TextOut(0, 25, coordinates);

	CString coordinates2;
	coordinates2.Format(_T("Left = %d Right = %d Top = %d Bottom = %d"), clientR.left, clientR.right, clientR.top, clientR.bottom);
	dc->TextOut(0, 50, coordinates2);
}

void CMainFrame::OnMove(int x, int y){

	CFrameWnd::OnMove(x, y);
	CDC * dc; dc = GetDC();
	CRect clientR, windowR;

	CClientDC dc2(this);
	GetClientRect(&clientR);
	GetWindowRect(&windowR);

	int thicknessOfBorder = GetSystemMetrics(SM_CXPADDEDBORDER);
	int & t = thicknessOfBorder;

	CString message = _T("Hello from Ozlem Cinar");

	dc2.TextOut(0, 0, message);

	if (globalCounter > 1) {

		dc->SetTextColor(RGB(136, 168, 254));

	}
	globalCounter++;

	CString coordinates;

	coordinates.Format(_T("Left = %d, Right = %d, Top = %d, Bottom = %d"), windowR.left, windowR.right, windowR.top, windowR.bottom);
	dc->TextOut(0, 25, coordinates);

	CString coordinates2;

	coordinates2.Format(_T("Left = %d Right = %d Top = %d Bottom = %d"), clientR.left, clientR.right, clientR.top, clientR.bottom);
	dc->TextOut(0, 50, coordinates2);
}

CMainFrame::~CMainFrame(){

}



int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct){

	if (CFrameWnd::OnCreate(lpCreateStruct) == -1)
		return -1;

	if (!m_wndView.Create(NULL, NULL, AFX_WS_DEFAULT_VIEW, CRect(0, 0, 0, 0), this, AFX_IDW_PANE_FIRST, NULL)){

		TRACE0("Failed to create view window\n");
		return -1;
	}

	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE | CBRS_TOP | CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC) || !m_wndToolBar.LoadToolBar(IDR_MAINFRAME)){

		TRACE0("Failed to create toolbar\n");
		return -1;

	}

	if (!m_wndStatusBar.Create(this)){

		TRACE0("Failed to create status bar\n");
		return -1;

	}

	m_wndStatusBar.SetIndicators(indicators, sizeof(indicators) / sizeof(UINT));
	m_wndToolBar.EnableDocking(CBRS_ALIGN_ANY);

	EnableDocking(CBRS_ALIGN_ANY);

	DockControlBar(&m_wndToolBar);
	return 0;

}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs){

	if (!CFrameWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	cs.lpszClass = AfxRegisterWndClass(0);

	return TRUE;

}


#ifdef _DEBUG

void CMainFrame::AssertValid() const{

	CFrameWnd::AssertValid();

}

void CMainFrame::Dump(CDumpContext& dc) const{

	CFrameWnd::Dump(dc);

}

#endif 

void CMainFrame::OnSetFocus(CWnd*){
	m_wndView.SetFocus();

}

BOOL CMainFrame::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo){

	if (m_wndView.OnCmdMsg(nID, nCode, pExtra, pHandlerInfo))

		return TRUE;

	return CFrameWnd::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);

}