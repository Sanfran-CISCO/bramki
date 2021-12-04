#include "AndGateway.hpp"
#include "LogicGateway.hpp"
#include "pch.h"

AndGateway::AndGateway() : LogicGateway() { ; }

AndGateway::AndGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};

AndGateway::~AndGateway() { ; }

bool AndGateway::getOutput() {
	return LogicGateway::inputA && LogicGateway::inputB;
}