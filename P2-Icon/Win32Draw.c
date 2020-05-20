/* Created By Ozlem Cinar 
	on 09/26/2019 */

#include <Windows.h>
#include <tchar.h>
#include "Win32Draw.h"
#define MAX_LOADSTRING 100

HINSTANCE hInst; 
TCHAR szTitle[MAX_LOADSTRING]; 
TCHAR szWindowClass[MAX_LOADSTRING]; 
ATOM MyRegisterClass(HINSTANCE hInstance);
BOOL InitInstance(HINSTANCE, int);
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPTSTR lpCmdLine,
	_In_ int nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);
	MSG msg;
	HACCEL hAccelTable;
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_WIN32DRAW, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}
	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDI_MYICON));
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
	return (int)msg.wParam;
}
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wclassx;
	wclassx.cbSize = sizeof(WNDCLASSEX);
	wclassx.style = CS_HREDRAW | CS_VREDRAW;
	wclassx.lpfnWndProc = WndProc;
	wclassx.cbClsExtra = 0;
	wclassx.cbWndExtra = 0;
	wclassx.hInstance = hInstance;
	wclassx.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_MYICON));
	wclassx.hCursor = LoadCursor(NULL, IDC_ARROW);
	wclassx.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wclassx.lpszMenuName = MAKEINTRESOURCE(IDC_WIN32DRAW);
	wclassx.lpszClassName = szWindowClass;
	wclassx.hIconSm = (HICON)LoadImageW(GetModuleHandleW(NULL), L"myIcon.ico", IMAGE_ICON, GetSystemMetrics(SM_CXSMICON), GetSystemMetrics(SM_CYSMICON), LR_LOADFROMFILE);
	if (wclassx.hIconSm) {
		SendMessageW(wclassx.hIconSm, WM_SETICON, ICON_SMALL, (LPARAM)wclassx.hIconSm);
		SendMessageW(wclassx.hIconSm, WM_SETICON, ICON_BIG, (LPARAM)wclassx.hIconSm);
	}
	return RegisterClassEx(&wclassx);
}
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;
	hInst = hInstance; 
	hWnd = CreateWindowW(szWindowClass, _T("Drawn by Ozlem Cinar"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 2000, 2000, 0, 0, hInstance, 0);

	if (!hWnd)
	{
		return FALSE;
	}
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);
	return TRUE;
}
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	HPEN hPen;
	HPEN hOldPen;
	HBRUSH hBrush;
	HBRUSH hOldBrush;
	int h1 = rand() % 600;
	int w1 = rand() % 600;
	switch (message)
	{
	case WM_COMMAND:
		wmId = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case ID_DRAW_CIRCLE:
			hdc = GetDC(hWnd); 
			hPen = CreatePen(PS_SOLID, 3, RGB(0, 0, 255)); 
			hBrush = CreateHatchBrush(HS_DIAGCROSS, RGB(255, 0, 255));
			hOldPen = (HPEN)SelectObject(hdc, hPen);
			hOldBrush = (HBRUSH)SelectObject(hdc, hBrush); 
			Ellipse(hdc, h1, w1, h1 +30 , w1 + 30); 
			SelectObject(hdc, hOldBrush); 
			DeleteObject(hBrush); 
			SelectObject(hdc, hOldPen); 
			DeleteObject(hPen);
			ReleaseDC(hWnd, hdc); 
			break;
		case ID_DRAW_RECTANGLE:
			hdc = GetDC(hWnd); 
			hPen = CreatePen(PS_SOLID, 3, RGB(255, 0, 0)); 
			hBrush = CreateSolidBrush(RGB(64, 224, 208)); 
			hOldPen = (HPEN)SelectObject(hdc, hPen); 
			hOldBrush = (HBRUSH)SelectObject(hdc, hBrush); 
			Rectangle(hdc, h1, w1, h1 + 50, w1 + 25); 
			SelectObject(hdc, hOldBrush); 
			DeleteObject(hBrush); 
			SelectObject(hdc, hOldPen); 
			DeleteObject(hPen);
			ReleaseDC(hWnd, hdc); 
			break;
		case ID_DRAW_CLEARSCREEN:
			InvalidateRect(hWnd, NULL, TRUE);
			break;
		case ID_DRAW_QUIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;
	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}