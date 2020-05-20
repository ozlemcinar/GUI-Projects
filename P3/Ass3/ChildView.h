
#pragma once


class CChildView : public CWnd{
public:

	CChildView();

public:

public:

protected:

	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);


public:

	virtual ~CChildView();


protected:

	afx_msg void OnPaint();

	DECLARE_MESSAGE_MAP()

};