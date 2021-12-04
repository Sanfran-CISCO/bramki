#include "NotGateway.hpp"
#include "LogicGateway.hpp"
#include "pch.h"

NotGateway::NotGateway() : LogicGateway() { ; }

NotGateway::NotGateway(bool input) : LogicGateway(input, false) {};

NotGateway::~NotGateway() {;}

bool NotGateway::getOutput() {
	return !(LogicGateway::inputA);
}

void NotGateway::setInput(bool input) {
	LogicGateway::setInputs(input, false);
}