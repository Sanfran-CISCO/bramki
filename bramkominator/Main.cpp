#include <SFML/Graphics.hpp>
#include <iostream>
#include "Button.hpp"

int main() {
	sf::RenderWindow window(sf::VideoMode(900, 900, 32), "Bramkominator");

	sf::Event event;
	Button button({ 200, 100 }, {400, 400}, "Text");

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