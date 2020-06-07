class Customer {
  String name;
  List<Rental> rentals = List<Rental>();

  Customer(this.name);

  void addRental(Rental rental) {
    rentals.add(rental);
  }

  String statement() {
    double totalAmount = 0;
    int frequentRenterPoints = 0;
    String result = "Rental record for $name\n";

    for (final rental in rentals) {
      double amount = 0;
      switch (rental.movie.priceCode) {
        case Movie.REGULAR:
          amount += 2;
          if (rental.daysRented > 2) amount += (rental.daysRented - 2) * 1.5;
          break;
        case Movie.NEW_RELEASE:
          amount += rental.daysRented * 3;
          break;
        case Movie.CHILDREN:
          amount += 1.5;
          if (rental.daysRented > 3) amount += (rental.daysRented - 3) * 1.5;
          break;
      }

      // add frequent renter points
      frequentRenterPoints++;

      // add bonus for a two day new release rental
      if (rental.movie.priceCode == Movie.NEW_RELEASE && rental.daysRented > 1)
        frequentRenterPoints++;

      // show figures for this rental
      result += "\t ${rental.movie.title} \t $amount \n";

      totalAmount += amount;
    }
    result += "Amount owed is $totalAmount \n";
    result += "You earned $frequentRenterPoints frequent renter points";

    return result;
  }
}

class Rental {
  Movie movie;
  int daysRented;

  Rental(this.movie, this.daysRented);
}

class Movie {
  static const int CHILDREN = 2;
  static const int REGULAR = 0;
  static const int NEW_RELEASE = 1;

  String title;
  int priceCode;

  Movie(this.title, this.priceCode);
}
