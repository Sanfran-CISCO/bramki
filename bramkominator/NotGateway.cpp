#include "NotGateway.hpp"

NotGateway::NotGateway() { ; }

NotGateway::NotGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {}

NotGateway::~NotGateway() {;}

bool NotGateway::getOutput() {
	return !(LogicGateway::inputA);
}