#include "NotGateway.hpp"

NotGateway::NotGateway() { ; }

NotGateway::NotGateway(bool inputA, bool inputB) { ; }

NotGateway::NotGateway(bool input) {
	LogicGateway::inputA = input;
	LogicGateway::inputB = input;
}

NotGateway::~NotGateway() {;}

bool NotGateway::getOutput() {
	return !(LogicGateway::inputA);
}

void NotGateway::setInput(bool input) {
	LogicGateway::inputA = input;
	LogicGateway::inputB = input;
}