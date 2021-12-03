#pragma once

#ifndef LogicGateway_hpp
#define LogicGateway_hpp

class LogicGateway
{
protected:
	bool inputA;
	bool inputB;

public:

	LogicGateway();
	LogicGateway(bool inputA, bool inputB);
	~LogicGateway();

	void setInputs(bool inputA, bool inputB);
	virtual bool getOutput() = 0;
};

#endif