#include "NandGateway.hpp"

NandGateway::NandGateway() { ; }

NandGateway::NandGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};

NandGateway::~NandGateway() { ; }

bool NandGateway::getOutput() {
	return !(LogicGateway::inputA) || !(LogicGateway::inputB);
}