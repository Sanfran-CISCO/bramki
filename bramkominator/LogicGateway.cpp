#include "LogicGateway.hpp"

LogicGateway::LogicGateway() {
	this->inputA = false;
	this->inputB = false;
}

LogicGateway::LogicGateway(bool inputA, bool inputB) {
	this->inputA = inputA;
	this->inputB = inputB;
}

LogicGateway::~LogicGateway() {}

void LogicGateway::setInputs(bool inputA, bool inputB) {
	this->inputA = inputA;
	this->inputB = inputB;
}