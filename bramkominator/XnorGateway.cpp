#include "XnorGateway.hpp"

XnorGateway::XnorGateway() { ; }

XnorGateway::XnorGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};

XnorGateway::~XnorGateway() { ; }

bool XnorGateway::getOutput() {
	return LogicGateway::inputA == LogicGateway::inputB;
}