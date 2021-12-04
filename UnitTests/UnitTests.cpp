#include "pch.h"
#include "CppUnitTest.h"

#include "../LogicGateways/NotGateway.hpp"
#include "../LogicGateways/AndGateway.hpp"
#include "../LogicGateways/OrGateway.hpp"
#include "../LogicGateways/NandGateway.hpp"
#include "../LogicGateways/XorGateway.hpp"
#include "../LogicGateways/NorGateway.hpp"
#include "../LogicGateways/XnorGateway.hpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace LogicGatewaysUnitTests
{
	TEST_CLASS(LogicGatewaysUnitTests)
	{
	public:
		
		TEST_METHOD(NotGatewayTest)
		{
			NotGateway not1(true);
			NotGateway not2(false);

			Assert::AreEqual(false, not1.getOutput());
			Assert::AreEqual(true, not2.getOutput());

			not1.setInput(false);
			Assert::AreEqual(false, not2.getOutput());
		}

		TEST_METHOD(AndGatewayTest) {
			AndGateway and1(true, true);
			AndGateway and2(true, false);

			Assert::AreEqual(true, and1.getOutput());
			Assert::AreEqual(false, and2.getOutput());

			and1.setInputs(false, false);
			Assert::AreEqual(false, and1.getOutput());
		}

		TEST_METHOD(TAK) {
			OrGateway or1(true, true);
			OrGateway or2(true, false);
			OrGateway or3(false, false);

			Assert::AreEqual(true, or1.getOutput());
			Assert::AreEqual(true, or2.getOutput());
			Assert::AreEqual(false, or3.getOutput());
		}
	};
}
