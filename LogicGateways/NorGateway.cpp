#include "NorGateway.hpp"
#include "pch.h"

NorGateway::NorGateway() { ; }
NorGateway::NorGateway(bool inputA, bool inputB) : LogicGateway(inputA, inputB) {};
NorGateway::~NorGateway() { ; }

bool NorGateway::getOutput() {
	return !(LogicGateway::inputA) && !(LogicGateway::inputB);
}