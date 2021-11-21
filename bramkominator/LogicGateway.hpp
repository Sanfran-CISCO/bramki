#pragma once

#ifndef LogicGateway_hpp
#define LogicGateway_hpp

class LogicGateway
{
public:
	bool inputA;
	bool inputB;

	LogicGateway();
	LogicGateway(bool inputA, bool inputB);
	~LogicGateway();

	virtual bool getOutput() = 0;
	void setInputs(bool inputA, bool inputB);
};

#endif