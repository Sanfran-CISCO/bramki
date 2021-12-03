#include "pch.h"
#include "CppUnitTest.h"

#include "../bramkominator/NotGateway.hpp"
#include "../bramkominator/AndGateway.hpp"
#include "../bramkominator/OrGateway.hpp"
#include "../bramkominator/NandGateway.hpp"
#include "../bramkominator/XorGateway.hpp"
#include "../bramkominator/NorGateway.hpp"
#include "../bramkominator/XnorGateway.hpp"

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

		TEST_METHOD(OrGatewayTest) {
			OrGateway or1(true, true);
			OrGateway or2(true, false);
			OrGateway or3(false, false);

			Assert::AreEqual(true, or1.getOutput());
			Assert::AreEqual(true, or2.getOutput());
			Assert::AreEqual(false, or3.getOutput());
		}
	};
}
