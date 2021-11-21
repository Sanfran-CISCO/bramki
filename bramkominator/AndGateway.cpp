#include "AndGateway.hpp"

AndGateway::AndGateway() { ; }

AndGateway::AndGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};

AndGateway::~AndGateway() { ; }

bool AndGateway::getOutput() {
	return LogicGateway::inputA && LogicGateway::inputB;
}