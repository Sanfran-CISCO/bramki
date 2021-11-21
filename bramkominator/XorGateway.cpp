#include "XorGateway.hpp"

XorGateway::XorGateway() { ; }

XorGateway::XorGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};

XorGateway::~XorGateway() { ; }

bool XorGateway::getOutput() {
	return LogicGateway::inputA != LogicGateway::inputB;
}