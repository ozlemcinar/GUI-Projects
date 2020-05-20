/*
 Created By Ozlem Cinar on 09/09/2019
*/
#include <windows.h>
#include <stdlib.h>
#include <tchar.h>
#include <string.h>

static TCHAR szWindowClass[] = (L"MyWin32GUIProject");
static int y_position;
static int nHeight;
static int num_of_lines = 0;
static TCHAR szTitle[] = (L"MyWin32GUIProject");
HFONT hFont;
LPCTSTR fontArray[] = { L"Athelas" ,L"Ayuthaya",L"Bradley Hand",L"Century Gothic",
L"Courier New",L"Herculanum",L"Wide Latin",L"Times New Roman",L"Arial",L"Comic Sans MS"};
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
int WINAPI WinMain(HINSTANCE hInstance,
 HINSTANCE hPrevInstance,
 LPSTR lpCmdLine,
 int nCmdShow)
{
 WNDCLASSEX wcex;
 wcex.cbSize = sizeof(WNDCLASSEX);
 wcex.style = CS_HREDRAW | CS_VREDRAW;
 wcex.lpfnWndProc = WndProc;
 wcex.cbClsExtra = 0;
 wcex.cbWndExtra = 0;
 wcex.hInstance = hInstance;
 wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
 wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
 wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
 wcex.lpszMenuName = NULL;
 wcex.lpszClassName = szWindowClass;
 wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
 if (!RegisterClassEx(&wcex)){
  MessageBox(NULL,
   _T("Call to RegisterClassEx failed!"),
   _T("Win32 Guided Tour"),
   NULL);
  return 1;
 }
 HWND hWnd = CreateWindow(
  szWindowClass,
  szTitle,
  WS_OVERLAPPEDWINDOW,
  CW_USEDEFAULT, CW_USEDEFAULT,
  750, 750,
  NULL,
  NULL,
  hInstance,
  NULL
 );
 if (!hWnd)
 {
  MessageBox(NULL,
   _T("Call to CreateWindow failed!"),
   _T("Win32 Guided Tour"),
   NULL);
  return 1;
 }
 ShowWindow(hWnd, nCmdShow);
 UpdateWindow(hWnd);
 MSG msg;
 while (GetMessage(&msg, NULL, 0, 0)) {
  TranslateMessage(&msg);
  DispatchMessage(&msg);
 }
 return (int)msg.wParam;
}
void increment_num_of_lines(HDC hDC){
 num_of_lines++;
 y_position = (-1) * nHeight * num_of_lines; // allow some space to avoid clipping
};
void SetFont(HDC hDC){
 if (num_of_lines == 0) {
  hFont = CreateFont(36, 10, -300, 0, FW_DONTCARE, 0, FALSE, 0, ANSI_CHARSET, OUT_TT_PRECIS,
   CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Times New Roman"));
 }
}
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam){
 PAINTSTRUCT ps;
 HDC hdc;
 TCHAR messageFromOzlem[] = (L"\nHello from Ozlem!\n");
 RECT rect;
 switch (message){
  case WM_PAINT: {
   hdc = BeginPaint(hWnd, &ps);
   nHeight = -MulDiv(10, GetDeviceCaps(hdc, LOGPIXELSY), 72);
   GetClientRect(hWnd, &rect);
   TextOut(hdc, 5, y_position, messageFromOzlem, _tcslen(messageFromOzlem));
   EndPaint(hWnd, &ps);
  }
  case WM_LBUTTONDOWN: {
   InvalidateRect(hWnd, NULL, FALSE); // TRUE => erase current content
   hdc = BeginPaint(hWnd, &ps);
   GetClientRect(hWnd, &rect);
   hFont = CreateFont(13, 7, 0, 0, FW_DONTCARE, 0, FALSE, 0, ANSI_CHARSET, OUT_TT_PRECIS,
    CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, fontArray[rand()%10]);
   SelectObject(hdc, hFont);
   SetTextColor(hdc, RGB(rand() % 256, rand() % 256, rand() % 256));
   increment_num_of_lines(hdc);
   TextOut(hdc, 5, y_position, _T("Hello!"), _tcslen(L"Hello!"));
   DeleteObject(hFont);
   EndPaint(hWnd, &ps);   
   break;
  }
  case WM_DESTROY:  
   PostQuitMessage(0);   
   break;
  default:  
   return DefWindowProc(hWnd, message, wParam, lParam);    
   break;
 }
 return -1; 
}