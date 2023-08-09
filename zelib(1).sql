-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 30, 2023 at 01:29 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `zelib`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `name` text NOT NULL,
  `type` text NOT NULL,
  `total_downloads` text NOT NULL,
  `books_upload` text NOT NULL,
  `bio` text NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `profile_image` text NOT NULL,
  `machine_id` text NOT NULL,
  `register_date` datetime NOT NULL,
  `total_likes` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`name`, `type`, `total_downloads`, `books_upload`, `bio`, `username`, `password`, `profile_image`, `machine_id`, `register_date`, `total_likes`) VALUES
('Zekkel AR', 'user', '0', '0', 'Python Programmer | C# Programmer | metallurgist', 'zekkelar', 'root', 'http://127.0.0.1/img/c9af7f75-d6c4-4308-afaf-27673d5533b5.png', 'BFEBFBFF000906A322533448', '2023-07-26 23:19:49', '');

-- --------------------------------------------------------

--
-- Table structure for table `angkatan`
--

CREATE TABLE `angkatan` (
  `angkatan` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `bookmark_journal_external`
--

CREATE TABLE `bookmark_journal_external` (
  `added_date` text NOT NULL,
  `title` text NOT NULL,
  `author` text NOT NULL,
  `description` text NOT NULL,
  `publication_date` text NOT NULL,
  `language` text NOT NULL,
  `source` text NOT NULL,
  `link_download` text NOT NULL,
  `img_url` text NOT NULL,
  `username` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `bookmark_product`
--

CREATE TABLE `bookmark_product` (
  `identifier` text NOT NULL,
  `username` text NOT NULL,
  `datetime` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bookmark_product`
--

INSERT INTO `bookmark_product` (`identifier`, `username`, `datetime`) VALUES
('a85f82fc-3960-46cf-895f-e23c4c5b8583.png', 'zekkelar', '2023-07-29 08:48:55.237891'),
('bcd63641-6109-4819-b8ed-04c7b52e3658.png', 'zekkelar', '2023-07-30 00:00:15.083835');

-- --------------------------------------------------------

--
-- Table structure for table `books_`
--

CREATE TABLE `books_` (
  `image` text NOT NULL,
  `author` text NOT NULL,
  `username` text NOT NULL,
  `title` text NOT NULL,
  `years` text NOT NULL,
  `deskripsi` text NOT NULL,
  `category` text NOT NULL,
  `datetime` datetime NOT NULL,
  `pdf` text NOT NULL,
  `item_id` text NOT NULL,
  `views` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `books_`
--

INSERT INTO `books_` (`image`, `author`, `username`, `title`, `years`, `deskripsi`, `category`, `datetime`, `pdf`, `item_id`, `views`) VALUES
('http://127.0.0.1/img/bcd63641-6109-4819-b8ed-04c7b52e3658.png', 'Jordy Lasmana Putra1, Syarah Seimahuira2', 'zekkelar', 'Memprediksi Pola Ban Hero Pada Game Mobile Legends Menggunakan Algoritma Apriori', '2020', 'Di era digital seperti sekarang ini, perkembangan video game begitu pesat, dari mulai yang berbasis console sampai ke perangkat smartphone. Salah satu genre video game yang sedang trend adalah Multiplayer Online Battle Arena (MOBA) dengan salah satu game MOBA yang digandrungi yaitu game Mobile Legends.\n\nDalam memenangkan sebuah pertandingan di dalam game Mobile Legends, diperlukan strategi permainan yang baik dari masing-masing tim untuk mempertahankan base dan menghancurkan base lawan. Salah satunya adalah dengan melakukan Ban Hero atau melarang beberapa hero yang ada agar tidak bisa digunakan baik untuk tim lawan maupun tim sendiri. Oleh karena itu, penelitian ini dilakukan untuk memprediksi ', 'Metallurgy', '2023-07-29 08:09:48', '127.0.0.1/pdf/3b115458-485c-433b-9fd5-ed7fe29fdad8.pdf', '0', '15'),
('http://127.0.0.1/img/39783767-377d-4230-9744-1fad9d34d703.png', 'MAKAT KATOM, OK TEDI MINING LIMITED; MURRAY WOOD AND MICHAEL SCHAFFER, SGS', 'zekkelar', 'APPLICATION AT OK TEDI MINING OF A NEURAL NETWORK MODEL WITHIN THE EXPERT SYSTEM FOR SAG MILL CONTROL', '2003', 'Dalam mengendalikan unit operasi proses mineral, sistem pakar yang diterapkan, oleh sifatnya, merupakan praktik operasi terbaik di lokasi tersebut, yang telah dirancang oleh para ahli lokal bersama dengan ahli metalurgi konsultasi. Lapisan \"akal sehat\" dalam solusi MET (Teknologi Ahli MinnovEX) menentukan tindakan yang tepat untuk kondisi operasi saat ini melalui model heuristik yang dikembangkan dari proses tersebut (sistem pakar). Besarnya perubahan setpoint yang dilakukan berbanding lurus dengan kepercayaan sistem terhadap interpretasinya terhadap kondisi saat ini (logika fuzzy). Solusi tersebut selalu berusaha untuk mengarahkan proses ke batas kendala dan dengan demikian mengoptimalkan oper', 'Metallurgy', '2023-07-29 08:13:56', '127.0.0.1/pdf/aca1ab26-f47e-4b32-b8b6-1fe023f78e07.pdf', '0', '9'),
('http://127.0.0.1/img/a85f82fc-3960-46cf-895f-e23c4c5b8583.png', 'Mulyanto Soerjodibroto1, Victor Amrizal1, Wishnu Prabowo1', 'zekkelar', 'APLIKASI ARTIFICIAL NEURAL NETWORK UNTUK KONTROL KONSUMSI BAHAN BAKAR MINYAK ALAT BERAT PADA KEGIATAN ROCKBREAKING', '2022', 'Pemanfaatan artificial intelligence (AI) dalam kegiatan penambangan memberikan banyak manfaat, terutama karena lingkungan kerja dalam pertambangan sering kali penuh dengan ketidakpastian dan variasi kondisi alam yang kompleks. Beberapa aplikasi AI dalam industri pertambangan termasuk otomasi peralatan, analisis dan pemrosesan data, identifikasi pola dan fitur data, serta penentuan solusi untuk meningkatkan efisiensi dan mengoptimalkan operasi.\n\nSalah satu aplikasi AI yang telah berhasil diimplementasikan adalah kontrol konsumsi bahan bakar minyak (BBM) dalam kegiatan Rock Breaking pada quarry batu kapur di daerah Sukabumi. Pemanfaatan AI dalam hal ini membantu dalam meramalkan konsumsi BBM deng', 'Metallurgy', '2023-07-29 08:15:39', '127.0.0.1/pdf/ad969413-5254-44ec-8046-f31b9c2b0906.pdf', '0', '4'),
('http://127.0.0.1/img/f9f430b3-8a7b-4166-a598-037cf4d8df53.png', 'Richard A. Arterburn', 'zekkelar', 'THE SIZING AND SELECTION OF HYDROCYCLONES', '2020', 'For many years, hydrocyclones, commonly referred to as cyclones, have been extensively utilized in the classification of particles in comminution circuits. The practical range of classification for cyclones is 40 microns to 400 microns, with some remote applications as fine as 5 microns or as coarse as 1000 microns. Cyclones are used in both primary and secondary grinding circuits as well as regrind circuits.\n\nThe information given in this paper is intended to provide a method, at least for estimating purposes, of selecting the proper number and size of cyclones and to determine the proper level of operating variables. Generally, it is recommended that cyclone suppliers be consulted for sizing ', 'Metallurgy', '2023-07-29 09:44:53', '127.0.0.1/pdf/151a6c28-0ace-4c00-b95a-0e2b5e9eacec.pdf', '0', '2');

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `categories` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `download_journal_external`
--

CREATE TABLE `download_journal_external` (
  `added_date` text NOT NULL,
  `author` text NOT NULL,
  `description` text NOT NULL,
  `img_url` text NOT NULL,
  `language` text NOT NULL,
  `link_download` text NOT NULL,
  `publication_date` text NOT NULL,
  `source` text NOT NULL,
  `title` text NOT NULL,
  `username` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `download_journal_external`
--

INSERT INTO `download_journal_external` (`added_date`, `author`, `description`, `img_url`, `language`, `link_download`, `publication_date`, `source`, `title`, `username`) VALUES
('2013-09-18T17:45:26Z', 'Andreas de Vries', 'In this paper, a short survey about the concepts underlying general logics is given. In particular, a novel rigorous definition of a fuzzy negation as an operation acting on a lattice to render it into a fuzzy logic is presented. According to this definition, a fuzzy negation satisfies the weak double negation condition, requiring double negation to be expansive, the antitony condition, being equivalent to the disjunctive De Morgan law and thus warranting compatibility of negation with the lattice operations, and the Boolean boundary condition stating that the universal bounds of the lattice are the negation of each other. From this perspective, the most general logics are fuzzy logics, containing as special cases paraconsistent (quantum) logics, quantum logics, intuitionistic logics, and Boolean logics, each of which given by its own algebraic restrictions. New examples of a non-contradictory logic violating the conjunctive De Morgan law, and of a typical non-orthomodular fuzzy logic...', 'https://archive.org/services/img/arxiv-0707.2161', '', 'https://archive.org/download/arxiv-0707.2161/0707.2161.pdf', '2013-09-18T17:45:26Z', 'http://arxiv.org/abs/0707.2161v1', 'Algebraic hierarchy of logics unifying fuzzy logic and quantum logic', 'zekkelar'),
('2023-02-23T11:48:16Z', '', 'Rope Bondage', 'https://archive.org/services/img/better-bondage-for-every-body-with-rope-bondage-experts-from-around-the-world', '', 'https://archive.org/download/better-bondage-for-every-body-with-rope-bondage-experts-from-around-the-world/better-bondage-for-every-body-with-rope-bondage-experts-from-around-the-world.pdf', '2023-02-23T11:48:16Z', '', 'Better Bondage For Every Body With Rope Bondage Experts From Around The World', 'zekkelar'),
('2013-05-23T03:32:55Z', '', 'Commodore C64 Manual: Commodore 64 Programmers Reference Guide (1983)(Commodore)', 'https://archive.org/services/img/Commodore_64_Programmers_Reference_Guide_1983_Commodore', '', 'https://archive.org/download/Commodore_64_Programmers_Reference_Guide_1983_Commodore/Commodore_64_Programmers_Reference_Guide_1983_Commodore.pdf', '2013-05-23T03:32:55Z', '', 'Commodore C64 Manual: Commodore 64 Programmers Reference Guide (1983)(Commodore)', 'zekkelar'),
('2013-04-01T16:38:55Z', '', '', 'https://archive.org/services/img/Peter_Norton_Inside_the_IBM_PC_Revised_and_Enlarged', '', 'https://archive.org/download/Peter_Norton_Inside_the_IBM_PC_Revised_and_Enlarged/Peter_Norton_Inside_the_IBM_PC_Revised_and_Enlarged.pdf', '2013-04-01T16:38:55Z', '', 'Peter Norton: Inside the IBM PC (Revised and Enlarged)', 'zekkelar'),
('2020-09-18T15:34:25Z', 'Verein zum Erhalt klassischer Computer e.V.', 'Schwerpunkt Retro-Computer und Emulation CBM 700 wird PC­-kompatibel Enterprise 128 emuliert ZX Spectrum Mac Emulator Spectre GCR PC­-Karten für den Atari ST Arduino Apple 1 Emulation Vier PDP­-11 Emulatoren PC-Karten in Acorn Computern Amiga Transformer Welt und Gesellschaft Archäologie der Digitalisierung Eine PDP­-8 zieht um Hardware USB und IDE für Atari ST Die Space Chase Story Fernschreibmaschinen heute Das FPGASID Projekt Praxis Flash Floppy­ Firmware für Gotek Messen mit dem Logikanalysator Vereinsleben Interview mit Stephan Kraus VzEkC e.V. News Rückschau Classic Computing 2018', 'https://archive.org/services/img/load-magazin-05', '', 'https://archive.org/download/load-magazin-05/load-magazin-05.pdf', '2020-09-18T15:34:25Z', '', 'Load Magazin 05', 'zekkelar'),
('2013-05-28T00:20:53Z', 'Morgan, Christopher P', 'Byte Book of Computer Music, The (1979)(Byte Publications)', 'https://archive.org/services/img/Byte_Book_of_Computer_Music_The_1979_Byte_Publications', '', 'https://archive.org/download/Byte_Book_of_Computer_Music_The_1979_Byte_Publications/Byte_Book_of_Computer_Music_The_1979_Byte_Publications.pdf', '2013-05-28T00:20:53Z', '', 'The Byte book of computer music', 'zekkelar'),
('2013-11-20T08:47:25Z', 'Vogel, James, 1952-Scrimshaw, Nevin B. (Nevin Baker), 1950-', 'The Commodore 64 Music Book', 'https://archive.org/services/img/The_Commodore_64_Music_Book', '', 'https://archive.org/download/The_Commodore_64_Music_Book/The_Commodore_64_Music_Book.pdf', '2013-11-20T08:47:25Z', '', 'The Commodore 64 music book : a guide to programming music and sound', 'zekkelar'),
('2013-09-17T20:17:14Z', 'Grigorii Melnichenko', 'In the paper, we show that quantum logic of linear subspaces can be used for recognition of random signals by a Bayesian energy discriminant classifier. The energy distribution on linear subspaces is described by the correlation matrix of the probability distribution. We show that the correlation matrix corresponds to von Neumann density matrix in quantum theory. We suggest the interpretation of quantum logic as a fuzzy logic of fuzzy sets. The use of quantum logic for recognition is based on the fact that the probability distribution of each class lies approximately in a lower-dimensional subspace of feature space. We offer the interpretation of discriminant functions as membership functions of fuzzy sets. Also we offer the quality functional for optimal choice of discriminant functions for recognition from some class of discriminant functions.', 'https://archive.org/services/img/arxiv-0711.1437', '', 'https://archive.org/download/arxiv-0711.1437/0711.1437.pdf', '2013-09-17T20:17:14Z', 'http://arxiv.org/abs/0711.1437v2', 'Energy Discriminant Analysis, Quantum Logic, and Fuzzy sets', 'zekkelar'),
('2013-09-18T21:24:24Z', 'J. ZhanW. A. DudekY. B. Jun', 'We introduce the concept of quasi-coincidence of a fuzzy interval value with an interval valued fuzzy set. By using this new idea, we introduce the notions of interval valued $(\\in,\\ivq)$-fuzzy filters of pseudo $BL$-algebras and investigate some of their related properties. Some characterization theorems of these generalized interval valued fuzzy filters are derived. The relationship among these generalized interval valued fuzzy filters of pseudo $BL$-algebras is considered. Finally, we consider the concept of implication-based interval valued fuzzy implicative filters of pseudo $BL$-algebras, in particular, the implication operators in Lukasiewicz system of continuous-valued logic are discussed.', 'https://archive.org/services/img/arxiv-0803.2338', '', 'https://archive.org/download/arxiv-0803.2338/0803.2338.pdf', '2013-09-18T21:24:24Z', 'http://arxiv.org/abs/0803.2338v1', 'Interval valued $(\\in,\\ivq)$-fuzzy filters of pseudo $BL$-algebras', 'zekkelar');

-- --------------------------------------------------------

--
-- Table structure for table `download_journal_internal`
--

CREATE TABLE `download_journal_internal` (
  `datetime` datetime NOT NULL,
  `identifier` text NOT NULL,
  `username` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `liked_product`
--

CREATE TABLE `liked_product` (
  `identifier` text NOT NULL,
  `username` text NOT NULL,
  `datetime` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
