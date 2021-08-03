class Customer {
  Customer(this.name);

  String name;
  List<Rental> rentals = <Rental>[];

  void addRental(Rental rental) {
    rentals.add(rental);
  }

  String statement() {
    String result = "Rental record for $name\n";
    rentals.forEach((r) {
      result += r.describe();
    });

    result += "Amount owed is ${totalAmount()} \n";
    result += "You earned ${frequentRenterPoints} frequent renter points";
    return result;
  }

  double totalAmount() => rentals.fold(0, (total, r) => total += r.price());

  int frequentRenterPoints() =>
      rentals.fold(0, (total, r) => total += r.frequentRenterPoints());
}

class Rental {
  const Rental(this.movie, this.daysRented);

  final Movie movie;
  final int daysRented;

  double price() => movie.price(daysRented);

  int frequentRenterPoints() => movie.frequentRenterPoints(daysRented);

  String describe() => "\t ${movie.title} \t ${price()} \n";
}

abstract class Movie {
  const Movie(this.title);

  final String title;

  double price(int days);
  int frequentRenterPoints(int days);
}

class NewReleaseMovie implements Movie {
  const NewReleaseMovie(this.title);

  final String title;

  @override
  double price(int days) => days * 3.0;

  @override
  int frequentRenterPoints(int days) => days > 1 ? 2 : 1;
}

class RegularMovie implements Movie {
  const RegularMovie(this.title);

  final String title;

  @override
  double price(int days) => days > 2 ? (days - 2) * 1.5 : 2.0;

  @override
  int frequentRenterPoints(int days) => 1;
}

class ChildrenMovie implements Movie {
  const ChildrenMovie(this.title);

  final String title;

  @override
  double price(int days) {
    var total = 1.5;
    if (days > 3) {
      total += (days - 3) * 1.5;
    }
    return total;
  }

  @override
  int frequentRenterPoints(int days) => 1;
}

// class Movie {
//   static const int CHILDREN = 2;
//   static const int REGULAR = 0;
//   static const int NEW_RELEASE = 1;

//   String title;
//   int priceCode;

//   Movie(this.title, this.priceCode);
// }

// double totalAmount = 0;
// int frequentRenterPoints = 0;
// String result = "Rental record for $name\n";

// for (final rental in rentals) {
//   double amount = 0;
//   switch (rental.movie.priceCode) {
//     case Movie.REGULAR:
//       amount += 2;
//       if (rental.daysRented > 2) amount += (rental.daysRented - 2) * 1.5;
//       break;
//     case Movie.NEW_RELEASE:
//       amount += rental.daysRented * 3;
//       break;
//     case Movie.CHILDREN:
//       amount += 1.5;
//       if (rental.daysRented > 3) amount += (rental.daysRented - 3) * 1.5;
//       break;
//   }

//   // add frequent renter points
//   frequentRenterPoints++;

//   // add bonus for a two day new release rental
//   if (rental.movie.priceCode == Movie.NEW_RELEASE && rental.daysRented > 1)
//     frequentRenterPoints++;

//   // show figures for this rental
//   result += "\t ${rental.movie.title} \t $amount \n";

//   totalAmount += amount;
// }
// result += "Amount owed is $totalAmount \n";
// result += "You earned $frequentRenterPoints frequent renter points";

// return result;
