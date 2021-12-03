#include <SFML/Graphics.hpp>
#include <iostream>
#include "Button.hpp"

#include "AndGateway.hpp"
#include "OrGateway.hpp"
#include "NandGateway.hpp"
#include "XorGateway.hpp"
#include "XnorGateway.hpp"
#include "NorGateway.hpp"

#include <string>

int main() {
	sf::RenderWindow window(sf::VideoMode(900, 900, 32), "Bramkominator");

	sf::Event event;

	OrGateway* andGateway = new OrGateway(true, false);

	sf::Color color = (andGateway->getOutput() == 1) ? sf::Color::Green : sf::Color::Red;

	Button button({ 200, 100 }, { 400, 400 }, "test");
	button.setButtonBackgroundColor(color);

	while (window.isOpen()) {
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed)
				window.close();
		}


		window.clear(sf::Color::Black);
		button.draw(window);
		window.display();
	}

	return 0;
}