#include "LogicGateway.hpp"
#include "pch.h"

LogicGateway::LogicGateway() : inputA(false), inputB(false){}

LogicGateway::LogicGateway(bool inputA, bool inputB) : inputA(inputA), inputB(inputB){}

LogicGateway::~LogicGateway() {}

void LogicGateway::setInputs(bool inputA, bool inputB) {
	this->inputA = inputA;
	this->inputB = inputB;
}