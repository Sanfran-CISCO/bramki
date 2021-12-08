#include "OrGateway.hpp"
#include "pch.h"

OrGateway::OrGateway() { ; }

OrGateway::OrGateway(bool inputA, bool inputB) :LogicGateway(inputA, inputB) {};

OrGateway::~OrGateway() { ; }

bool OrGateway::getOutput() {
	return LogicGateway::inputA || LogicGateway::inputB;
}