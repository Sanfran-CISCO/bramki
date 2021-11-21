#include "NorGateway.hpp"

NorGateway::NorGateway() { ; }
NorGateway::NorGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};
NorGateway::~NorGateway() { ; }

bool NorGateway::getOutput() {
	return !(LogicGateway::inputA) && !(LogicGateway::inputB);
}